using DomainLayer.Models.CartModule;
using Shared.DataTransferObjects.CartDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
  public  interface IBaskettService
    {

        Task<BasketDTO> GetCartAsync (string key);    
        Task<BasketDTO> CreateOrUpdateCart(BasketDTO cartDTO);

        Task<bool> DeleteCartAsync(string Key);


    }
}
