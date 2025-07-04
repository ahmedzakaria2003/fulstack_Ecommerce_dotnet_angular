using Shared.DataTransferObjects.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
  public interface IOrderService
    {
        Task<OrderToReturnDTO> CreateOrderAsync (OrderDTO orderDTO , string Email );

        // Delivery Methods
        Task<IEnumerable<DeliveryMethodsDTO>> GetDeliveryMethodsAsync();
        // Get All Orders
        Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync(string Email);
        // Get Order By Id
        Task<OrderToReturnDTO> GetOrderByIdAsync(Guid Id);
    }
}
