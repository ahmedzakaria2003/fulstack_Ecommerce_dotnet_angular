using AutoMapper;
using DomainLayer.Models;
using Microsoft.Extensions.Options;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {

        public ProductProfile() {

            CreateMap<Product, ProductDTO>()
                    .ForMember(dist => dist.ProductBrand, Options => Options.MapFrom(Src => Src.ProductBrand.Name))
                    .ForMember(dist => dist.ProductType, Options => Options.MapFrom(Src => Src.ProductType.Name))
                    .ForMember(dist => dist.PictureUrl, Options => Options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDTO>();
            CreateMap<ProductType, TypeDTO>();
        }

    }
}
