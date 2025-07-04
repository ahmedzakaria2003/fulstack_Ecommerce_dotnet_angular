using Shared.DataTransferObjects.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.OrderDTOS
{
  public class OrderToReturnDTO
    {

        public Guid Id { get; set; }

        public string buyerEmail { get; set; } = default!;

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public string DeliveryMethod { get; set; } = default!;


        public AdressDTO shipToAddress { get; set; } = default!;

        public ICollection<OrderItemDTO> OrderItems { get; set; } = [];

        public decimal deliveryCost { get; set; }
        public string Status { get; set; } = default!;

        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }

    }
}
