using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.WishlistDTO
{
    public class WishlistDTO
    {

        public string Id { get; set; } = null!;
        public ICollection<WishlistItemDTO> Items { get; set; } = [];

    }
}
