using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager _serviceManager) : ApiBaseController 
    {

        // Create or Update payment intent Id
        [Authorize]
        [HttpPost("{CartId}")]
        public async Task<IActionResult> CreateOrUpdatePaymentIntentAsync(string CartId)
        {
            var Basket = await _serviceManager.payementService.CreateOrUpdatePaymentIntentAsync(CartId);
            return Ok(Basket);
        }

      

        [HttpPost("WebHook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var StripeHeader = Request.Headers["Stripe-Signature"];

         await   _serviceManager.payementService.UpdateOrderPaymentStatusAsync(json, StripeHeader!);
            return new  EmptyResult();

        }
    }


}
