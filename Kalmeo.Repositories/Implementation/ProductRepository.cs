using Kalmeo.Models;
using Kalmeo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalmeo.Repositories.Implementation
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(KalmeoContext kalmeoContext) : base(kalmeoContext)
        {
        }
    }
}
