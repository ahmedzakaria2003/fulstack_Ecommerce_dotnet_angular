﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.OrderDTOS
{
    public class DeliveryMethodsDTO
    {

        public int Id { get; set; }
        public string ShortName { get; set; } = default!;
        public string DeliveryTime { get; set; } = default!;
        public decimal Price { get; set; }
        public string Description { get; set; } = default!;
    }
}
