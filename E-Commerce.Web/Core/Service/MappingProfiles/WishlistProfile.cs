using AutoMapper;
using DomainLayer.Models.CartModule;
using Shared.DataTransferObjects.CartDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
           CreateMap<CustomerBasket , BasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
       

        }
    }
}
