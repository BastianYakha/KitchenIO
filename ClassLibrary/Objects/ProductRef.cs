﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenIO.Objects
{
    public class ProductRef
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public string meassurement {  get; set; }
    }
}
