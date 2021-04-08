using AutoMapper;
using HbCampaignModule.Domain.Entities;
using HbCampaignModule.Domain.Model;

namespace HbCampaignModule.API.MapperProfile
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            #region Product Mapper
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            #endregion

            #region Order Mapper
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            #endregion

            #region Campaign Mapper
            CreateMap<Campaign, CampaignDto>();
            CreateMap<CampaignDto, Campaign>();
            #endregion


        }

    }
}
