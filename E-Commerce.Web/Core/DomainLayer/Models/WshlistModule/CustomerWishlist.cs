using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.WshlistModule
{
    public class CustomerWishlist
    {
        public string Id { get; set; } = null!; // ممكن يكون Email أو sessionKey
        public ICollection<WishlistItem> Items { get; set; } = [];

    }
}
