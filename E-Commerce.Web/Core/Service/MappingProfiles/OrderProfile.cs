using AutoMapper;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using Shared.DataTransferObjects.IdentityDTOS;
using Shared.DataTransferObjects.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AdressDTO, OrderAddress>().ReverseMap();

         CreateMap<Order, OrderToReturnDTO>()
       .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodsDTO>();


        }
    }
}