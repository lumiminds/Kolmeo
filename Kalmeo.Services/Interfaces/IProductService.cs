using Kalmeo.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalmeo.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> IsProductExistsAsync(Guid id);

        Task<ProductViewModel> CreateProductAsync(ProductViewModel productViewModel);

        Task<ProductViewModel> GetProductByIdAsync(Guid id);

        Task<ProductViewModel> UpdateProductAsync(ProductViewModel productViewModel);

        Task DeleteAsync(Guid id);

        Task<IEnumerable<ProductViewModel>> GetAllAsync();
    }
}
