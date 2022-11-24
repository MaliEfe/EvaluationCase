using AutoMapper;
using MediatR;
using EvaluationCase.Application.Features.Commands.CreateCampaign;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Application.Wrappers;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EvaluationCase.Domain.Entities;
using System.ComponentModel;

namespace EvaluationCase.Application.Features.Commands.AddBasket
{
    public class AddBasketCommandHandler : IRequestHandler<AddBasketCommand, ServiceResponse<Guid>>
    {
        IBasketRepository basketRepository;
        IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ICacheStore _cacheStore;
        const string cacheKey = "BASKETS";

        public AddBasketCommandHandler(IBasketRepository basketRepository, IProductRepository productRepository, IMapper mapper, ICacheStore cacheStore)
        {
            this.basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.mapper = mapper;
            _cacheStore = cacheStore;
        }

        public async Task<ServiceResponse<Guid>> Handle(AddBasketCommand request, CancellationToken cancellationToken)
        {
            List<BasketItem> basketList = new List<BasketItem>();

            foreach (var item in request.BasketItems)
            {
                var product = await productRepository.GetByIdAsync(item.ProductId);
                var quantity = item.Quantity;

                if (product != null)
                {
                    basketList.Add(new BasketItem
                    {
                        Product = product,
                        Quantity = quantity

                    });
                }
            }

            if (basketList.Count != 0)
            {
                var basket = new Basket { BasketItems = basketList };
                await basketRepository.CreateAsync(basket);

                await _cacheStore.RemoveAsync(new[] { cacheKey });

                return new ServiceResponse<Guid>(basket.Id);
            }

            throw new WarningException("Products not found");
        }
    }
}
