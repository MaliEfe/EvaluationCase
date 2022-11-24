using AutoMapper;
using MediatR;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Domain.Entities;
using EvaluationCase.Domain.Enums;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationCase.Application.Services
{
    public class CampaignEvaluateService
    {
        private readonly ICampaignRepository campaignRepository;
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;
        private readonly ICacheStore cacheStore;
        const string cacheKeyCampaign = "CAMPAIGNS";
        const string cacheKeyBasket = "BASKET_{0}";

        public CampaignEvaluateService(ICampaignRepository campaignRepository, IMapper mapper, ICacheStore cacheStore, IBasketRepository basketRepository)
        {
            this.campaignRepository = campaignRepository;
            this.mapper = mapper;
            this.cacheStore = cacheStore;
            this.basketRepository = basketRepository;
        }

        public async Task<BasketEvaluateReponse> CampaignEvaluate(Guid basketId)
        {
            BasketEvaluateReponse basketEvaluateReponse = new BasketEvaluateReponse();
            var allCampaigns = await GetCampaigns();
            var basketItems = await GetBasketItems(basketId);
            var activeCampaigns = allCampaigns.Where(a => a.IsActive && a.StartDate < DateTime.Now && a.EndDate > DateTime.Now);

            foreach (var campaign in activeCampaigns)
            {
                var response = SetCampaignsToBasket(campaign, basketItems);
                basketEvaluateReponse = FindCalculatedBestBasketItem(basketEvaluateReponse, response);
            }

            return basketEvaluateReponse;

        }

        private async Task<Basket> GetBasketItems(Guid basketId)
        {
            Basket basketItems = new Basket();
            var cachedData = await cacheStore.GetAsync<Basket>(string.Format(cacheKeyBasket, basketId));
            if (cachedData != null)
            {
                basketItems = cachedData;
            }
            else
            {
                basketItems = await basketRepository.GetByIdAsync(basketId);
                await cacheStore.AddAsync(basketItems, string.Format(cacheKeyBasket, basketId), TimeSpan.FromMinutes(5));
            }

            return basketItems;
        }

        public BasketEvaluateReponse SetCampaignsToBasket(Campaign campaign, Basket basket)
        {
            var products = basket.BasketItems.Select(a => a.Product).ToList();

            var matchingProducts = campaign.CampaignType switch
            {
                CampaignType.ProductGroup => products.Where(a => campaign.CampaignTypeValue.Any(p => a.ProductGroupCodes.Contains(p))),
                CampaignType.Brand => products.Where(a => campaign.CampaignTypeValue.Contains(a.Brand)),
                CampaignType.Category => products.Where(a => campaign.CampaignTypeValue.Contains(a.CategoryName.ToString())),
                CampaignType.ProductCode => products.Where(a => campaign.CampaignTypeValue.Contains(a.Code)),
                _ => products,
            };

            var calculatedBasketItemsResponse = CalculateDiscount(campaign, matchingProducts, basket);

            var basketEvaluateReponse = new BasketEvaluateReponse()
            {
                Items = calculatedBasketItemsResponse,
                TotalDiscountedPrice = calculatedBasketItemsResponse.Sum(a => a.Discount.DiscountedPrice)
            };

            return basketEvaluateReponse;
        }

        public List<BasketEvaluate> CalculateDiscount(Campaign campaign, IEnumerable<Product> products, Basket basket)
        {
            var ids = products.Select(a => a.Id).ToList();
            List<BasketEvaluate> basketEvaluateList = new List<BasketEvaluate>();
            var totalBasketPrice = basket.BasketItems.Sum(a => (a.Quantity * a.Product.Price));
            foreach (var item in basket.BasketItems)
            {
                if (products.Contains(item.Product) && totalBasketPrice >= campaign.MinimumBasketPrice)
                {
                    var discount = GetCalculatDiscountPrice(campaign, item, products.Count());
                    BasketEvaluate basketItemResponse = new BasketEvaluate()
                    {
                        ProductCode = item.Product.Code,
                        Price = item.Product.Price * item.Quantity,
                        Discount = new DiscountResponse()
                        {
                            DiscountPrice = discount,
                            CampaignId = campaign.Id.ToString(),
                            DiscountedPrice = (item.Product.Price * item.Quantity) - discount
                        }
                    };

                    basketEvaluateList.Add(basketItemResponse);
                }
                else
                {
                    BasketEvaluate basketItemResponse = new BasketEvaluate()
                    {
                        ProductCode = item.Product.Code,
                        Price = item.Product.Price * item.Quantity,
                        Discount = GetNullDiscount(item)
                    };

                    basketEvaluateList.Add(basketItemResponse);
                }
            }
            return basketEvaluateList;
        }

        private static decimal GetCalculatDiscountPrice(Domain.Entities.Campaign campaign, BasketItem basketItemRequest, int productCount)
        {
            int quantity = campaign.AffectedCount >= basketItemRequest.Quantity
                ? basketItemRequest.Quantity
                : campaign.AffectedCount;

            decimal discount;
            _ = campaign.DiscountType switch
            {
                CampaignDiscountType.Rate => discount = (basketItemRequest.Product.Price * campaign.Discount / 100) * quantity,
                CampaignDiscountType.Amount => discount = campaign.Discount / productCount,
                _ => discount = 0
            };

            return discount;
        }
        private static DiscountResponse GetNullDiscount(BasketItem basketItemRequest)
        {
            return new DiscountResponse()
            {
                DiscountPrice = 0,
                CampaignId = null,
                DiscountedPrice = (basketItemRequest.Product.Price * basketItemRequest.Quantity)
            };
        }
        private BasketEvaluateReponse FindCalculatedBestBasketItem(BasketEvaluateReponse basketEvaluateReponse, BasketEvaluateReponse response)
        {
            if (basketEvaluateReponse.Items == null)
            {
                basketEvaluateReponse = response;
            }
            else
            {
                if (basketEvaluateReponse.TotalDiscountedPrice > response.TotalDiscountedPrice)
                {
                    basketEvaluateReponse = response;
                }
            }

            return basketEvaluateReponse;
        }
        private async Task<List<Campaign>> GetCampaigns()
        {
            List<Campaign> allCampaigns = new List<Campaign>();
            var cachedData = await cacheStore.GetAsync<List<Campaign>>(cacheKeyCampaign);
            if (cachedData != null)
            {
                allCampaigns = cachedData;
            }
            else
            {
                allCampaigns = await campaignRepository.GetAllAsync();
                await cacheStore.AddAsync(allCampaigns, cacheKeyCampaign, TimeSpan.FromMinutes(5));
            }

            return allCampaigns;
        }
    }
}
