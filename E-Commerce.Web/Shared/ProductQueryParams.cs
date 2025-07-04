using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
        private  const int DefaultPageSize = 5;

        private const int MaxPageSize = 10;
        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public ProductSorting sort { get; set; }

        public string? search { get; set; }

        public int pageNumber { get; set; } = 1;


        private int pagesize = DefaultPageSize;

        public int PageSize
        {
            get { return pagesize; }
            set { pagesize = value > MaxPageSize ? MaxPageSize : value  ; }
        }







    }
}
