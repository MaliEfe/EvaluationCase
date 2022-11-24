using AutoMapper;
using EvaluationCase.Application.Features.Commands.AddBasket;
using EvaluationCase.Application.Features.Commands.CreateCampaign;
using EvaluationCase.Application.Features.Commands.CreateProduct;
using EvaluationCase.Application.Features.Commands.SetInActiveCampaign;
using EvaluationCase.Application.Features.Commands.UpdateCampaign;
using EvaluationCase.Application.Features.Commands.UpdateProduct;
using EvaluationCase.Application.Features.Queries.GetProductById;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Mapping
{
    public class GeneralMapping : Profile
    {

        public GeneralMapping()
        {
            CreateMap<Domain.Entities.Product, Dtos.ProductViewDto>()
                .ReverseMap();

            CreateMap<Domain.Entities.Product, CreateProductCommand>()
                .ReverseMap();

            CreateMap<Domain.Entities.Product, UpdateProductCommand>()
               .ReverseMap();

            CreateMap<Domain.Entities.Product, GetProductByIdViewModel>()
                .ReverseMap();

            CreateMap<Domain.Entities.Campaign, CreateCampaignCommand>()
                .ReverseMap();

            CreateMap<Domain.Entities.Campaign, SetInActiveCampaignCommand>()
                .ReverseMap();

            CreateMap<Domain.Entities.Campaign, Dtos.CampaignViewDto>()
                .ReverseMap();

            CreateMap<Domain.Entities.BasketItem, Dtos.BasketItemViewDto>()
                .ReverseMap();

            CreateMap<Domain.Entities.Basket, AddBasketCommand>()
               .ReverseMap();

            CreateMap<Domain.Entities.Basket, Dtos.BasketViewDto>()
           .ReverseMap();

            CreateMap<Domain.Entities.BasketEvaluate, Dtos.BasketEvaluateViewDto>()
           .ReverseMap();

            CreateMap<Domain.Entities.BasketEvaluateReponse, Dtos.BasketEvaluateReponseViewDto>()
           .ReverseMap();

            CreateMap<Domain.Entities.DiscountResponse, Dtos.DiscountResponseDto>()
           .ReverseMap();

        }

    }
}
