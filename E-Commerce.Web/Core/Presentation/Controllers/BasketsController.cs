using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.CartDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{


    public class BasketsController(IServiceManager _serviceManager) : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> GetBasket(string id)
        {
            var Cart = await _serviceManager.CartService.GetCartAsync(id);
            return Ok(Cart);

        }
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateCart(BasketDTO cartDTO)
        {
          
            var Cart = await _serviceManager.CartService.CreateOrUpdateCart(cartDTO);
            return Ok(Cart);
        }

        [HttpDelete("{key}")]

        public async Task<ActionResult<bool>> Deleted(string Key)
        {
            var IsDeleted = await _serviceManager.CartService.DeleteCartAsync(Key);
            return Ok(IsDeleted);

        }

        



    }
}
