using Shared.DataTransferObjects.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.OrderDTOS
{
    public class OrderDTO
    {
        [Required]
        public string CartId { get; set; } = default!;

        public int DeliveryMethodId { get; set; }

        public AdressDTO shipToAddress { get; set; } = default!;




    }
}
