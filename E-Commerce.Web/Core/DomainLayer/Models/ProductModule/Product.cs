﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }

        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; } //fk

        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; } //fk


    }
}
