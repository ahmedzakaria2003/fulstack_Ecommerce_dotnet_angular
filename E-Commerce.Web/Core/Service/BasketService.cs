using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.CartModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.CartDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketService(IBasketRepoistory _cartRepoistory , IMapper _mapper) : IBaskettService
    {
        public async Task<BasketDTO> CreateOrUpdateCart(BasketDTO cartDTO)
        {
            //(Input) Stored In Raduis ..Map From DTO(Front) To (Entity)
         var CustomerCart = _mapper.Map<BasketDTO, CustomerBasket>(cartDTO);
            var CreatedOrUpdatedCart = await _cartRepoistory.CreateOrUpdateCartAsync(CustomerCart);
            if (CreatedOrUpdatedCart is not null)
            {
                return await GetCartAsync(CreatedOrUpdatedCart.Id);
            }
            else
            {
                throw new CartNotFoundException("Can Not Update Or Create Cart Now");
            }
        }

        public async Task<bool> DeleteCartAsync(string Key)
        {
            return await _cartRepoistory.DeleteCartAsync(Key);
        }

        public async Task<BasketDTO> GetCartAsync(string key)
        {
            // Display Cart (Out Put) So Map From Entity To DTO
           var Cart = await _cartRepoistory.GetCartAsync(key);
            if (Cart is not null)
            {
                return _mapper.Map<CustomerBasket, BasketDTO>(Cart);
            }

            else
            {
                throw new CartNotFoundException(key);
            }
        }

      
    }

}
