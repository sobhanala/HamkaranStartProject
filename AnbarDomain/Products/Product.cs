﻿using Domain.Common;
using System;

namespace AnbarDomain.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public Unit UnitOfMeasure { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal? Weight { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}