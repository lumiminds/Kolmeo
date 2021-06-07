using System;

namespace Kalmeo.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
