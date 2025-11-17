using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfile
{
    public class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        string IValueResolver<Product, ProductResultDto, string>.Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl)) return string.Empty;
            return $"{ configuration["BaseUrl"]}/{source.PictureUrl}";
        }
    }
}
