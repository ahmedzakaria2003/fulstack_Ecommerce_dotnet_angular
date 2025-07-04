using Shared.DataTransferObjects.CartDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
   public interface IPayementService
    {
        Task<BasketDTO> CreateOrUpdatePaymentIntentAsync(string BasketId);

        Task UpdateOrderPaymentStatusAsync(string request, string header);

    }
}
