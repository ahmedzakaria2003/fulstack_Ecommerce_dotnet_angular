using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
   public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }

        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> orderItems, decimal subtotal, string paymentIntentId)
        {
            BuyerEmail = userEmail;
            shipToAddress = address;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; } = default!;

        public OrderAddress shipToAddress { get; set; } = default!;

        public DeliveryMethod DeliveryMethod { get; set; } = default!;

        public ICollection<OrderItem> OrderItems { get; set; } = [];

        public decimal Subtotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public int DeliveryMethodId { get; set; } // Foreign key for DeliveryMethod
        public OrderStatus Status { get; set; } 
        public decimal GetTotal() => Subtotal + DeliveryMethod.Price; // Not Mapped In Db 

        public string PaymentIntentId { get; set; }


    }
}
