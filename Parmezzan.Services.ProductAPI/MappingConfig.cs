using AutoMapper;
using Parmezzan.Services.ProductAPI.Models;
using Parmezzan.Services.ProductAPI.Models.Dto;

namespace Parmezzan.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });
        }
    }
}
