using System;

namespace Kalmeo.Models
{
    public class Product : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
