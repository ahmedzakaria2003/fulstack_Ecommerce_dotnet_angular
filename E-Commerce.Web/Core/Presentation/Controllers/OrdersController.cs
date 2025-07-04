using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
   public class OrdersController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Create Order
        [Authorize]
        [HttpPost]

        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO)
        {

            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Order = await _serviceManager.OrderService.CreateOrderAsync(orderDTO, Email!);
            return Ok(Order);
        }

        // Get Delivery Methods
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodsDTO>>> GetDeliveryMethods()
        {
            var DeliveryMethod = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok (DeliveryMethod);

        }

        // Get All Orders By Email
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetAllOrders()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await _serviceManager.OrderService.GetAllOrdersAsync(Email!);
            return Ok(Orders);
        }

        // Get Order By Id
        [Authorize]
        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderById(Guid Id)
        {
            var Order = await _serviceManager.OrderService.GetOrderByIdAsync(Id);
            return Ok(Order);
        }




    }
}
