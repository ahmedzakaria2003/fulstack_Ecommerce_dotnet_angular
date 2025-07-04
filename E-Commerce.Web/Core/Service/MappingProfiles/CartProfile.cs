using AutoMapper;
using DomainLayer.Models.CartModule;
using DomainLayer.Models.WshlistModule;
using Shared.DataTransferObjects.CartDTOS;
using Shared.DataTransferObjects.WishlistDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class WishlistProfile : Profile
    {
        public WishlistProfile()
        {
            CreateMap<WishlistItem, WishlistItemDTO>().ReverseMap();
            CreateMap<CustomerWishlist, WishlistDTO>().ReverseMap();



        }
    }
}
