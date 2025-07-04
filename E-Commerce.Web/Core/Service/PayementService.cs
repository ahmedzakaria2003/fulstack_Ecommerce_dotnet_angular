using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Service.Specification;
using ServiceAbstraction;
using Shared.DataTransferObjects.CartDTOS;
using Stripe;
using Stripe.Forwarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = DomainLayer.Models.Product;
namespace Service
{
    public class PayementService(IConfiguration configuration , 
        IBasketRepoistory basketRepoistory,
         IUnitOfWork unitOfWork , 
         IMapper mapper  ) 
        : IPayementService
    {
        public async Task<BasketDTO> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            // Configure Stripe : Install Package Stripe.net

            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];

            // Get Basket by BasketId from Redis Cache
            var basket = await basketRepoistory.GetCartAsync(BasketId)
               ?? throw new CartNotFoundException(BasketId);

            // Get Amount - Get Product + Delivery Methods Cost
            var ProductRepo =  unitOfWork.GetRepository<Product, int>();
            foreach (var item in basket.Items)
            {
                var product = await ProductRepo.GetbyIdAsync(item.Id);
                if (product == null)
                    throw new ProductNotFoundExeptions(item.Id);
                item.Price = product.Price;
            }
            ArgumentNullException.ThrowIfNull(basket.deliveryMethodId);
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetbyIdAsync(basket.deliveryMethodId.Value) 
                ?? throw new DeliveryMethodNotFoundException(basket.deliveryMethodId.Value);
            basket.ShippingPrice = deliveryMethod.Price;
            var totalAmount = (long)(basket.Items.Sum(item => item.Price * item.Quantity) 
                +deliveryMethod.Price ) * 100;

            // Create PaymentIntent with Stripe API[Create - Update]

            var PaymentService = new PaymentIntentService();
            if (basket.paymentIntentId is  null) //  Create
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = totalAmount,
                    Currency = "usd",
                    PaymentMethodTypes = ["card"],
           
                };
            var PaymentIntent = await PaymentService.CreateAsync(options); 
            basket.paymentIntentId = PaymentIntent.Id;
            basket.clientSecret = PaymentIntent.ClientSecret;
            }
            else // Update
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = totalAmount,
                };
                await PaymentService.UpdateAsync(basket.paymentIntentId, options);

            }

            // Save Basket to Redis Cache
            var updatedBasket = await basketRepoistory.CreateOrUpdateCartAsync(basket);

            return mapper.Map<BasketDTO>(updatedBasket);


        }

        public async Task UpdateOrderPaymentStatusAsync(string request, string header)
        {


            var endpointSecret = configuration.GetSection("StripeSettings")["EndPointSecret"];
            var  stripeEvent = EventUtility.ConstructEvent(request,header , endpointSecret , throwOnApiVersionMismatch:false);
            var PaymentIntent = stripeEvent.Data.Object as PaymentIntent;
            // Handle the event
            // If on SDK version < 46, use class Events instead of EventTypes
            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentPaymentFailed:
                    {
                        await UpdatePaymentFailedAsync(PaymentIntent!.Id);

                        break;
                        // Then define and call a method to handle the successful payment intent.
                        // handlePaymentIntentSucceeded(paymentIntent);
                    }

                case EventTypes.PaymentIntentSucceeded:
                    {
                        await UpdatePaymentSuccessedAsync(PaymentIntent!.Id);
                        break;
                        // Then define and call a method to handle the successful attachment of a PaymentMethod.
                        // handlePaymentMethodAttached(paymentMethod);
                    }
                // ... handle other event types
                default:
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }
            
         
        }

        private async Task UpdatePaymentFailedAsync(string paymentintentid)
        {

            var OrderRepo = unitOfWork.GetRepository<Order, Guid>();
            var order = await OrderRepo.GetbyIdAsync
          (new OrderWithPayementIntentIdSpecification (paymentintentid)) ?? throw new Exception();

            order.Status = OrderStatus.PaymentFailed;
            OrderRepo.Update(order);
            await unitOfWork.SaveChangesAsync();


        }

        private async Task UpdatePaymentSuccessedAsync(string paymentintentid)
        {
            var OrderRepo = unitOfWork.GetRepository<Order, Guid>();
            var order = await OrderRepo.GetbyIdAsync
          (new OrderWithPayementIntentIdSpecification(paymentintentid)) ?? throw new Exception();

            order.Status = OrderStatus.PaymentReceived;
            OrderRepo.Update(order);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
    