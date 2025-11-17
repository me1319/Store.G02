using AutoMapper;
using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfile
{
    public class ProductProfiles :Profile
    {
        public ProductProfiles()
        {
            CreateMap<Product, ProductResultDto>()
                .ForMember(s => s.BrandName, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(s => s.TypeName, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(s => s.PictureUrl, o => o.MapFrom<PictureUrlResolver>());
            CreateMap<ProductBrand,BrandResultDto>();
            CreateMap<ProductType,TypeResultDto>();
        }
      
    }
}
