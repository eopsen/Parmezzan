using AutoMapper;
using Parmezzan.Services.ShoppingCartAPI.Models;
using Parmezzan.Services.ShoppingCartAPI.Models.Dto;

namespace Parmezzan.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
                config.CreateMap<CartHeaderDto, CartHeader>().ReverseMap();
                config.CreateMap<CartDetailDto, CartDetail>().ReverseMap();
                config.CreateMap<CartDto, Cart>().ReverseMap();
            });
        }
    }
}
