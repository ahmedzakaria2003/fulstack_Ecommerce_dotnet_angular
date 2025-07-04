using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.CartModule
{
   public class BasketItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
        public string Name { get; set; } = null!;
      
    }
}
