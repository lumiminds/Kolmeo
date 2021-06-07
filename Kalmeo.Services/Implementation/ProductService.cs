using AutoMapper;
using Kalmeo.Repositories.Interfaces;
using Kalmeo.Services.Extensions;
using Kalmeo.Services.Interfaces;
using Kalmeo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalmeo.Services.Implementation
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository) : base(mapper, unitOfWork)
        {
            // Repository
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var result = await _productRepository.GetAllAsync();
            var productVMs = result.Enumerable.Select(c => c.ToViewModel(_mapper)).ToList();
            return productVMs;
        }

        public async Task<bool> IsProductExistsAsync(Guid id)
        {
            return await _productRepository.GetExistsAsync(c => c.Id == id);
        }

        public async Task<ProductViewModel> CreateProductAsync(ProductViewModel productViewModel)
        {
            var product = productViewModel.ToModel(_mapper);
            await _productRepository.CreateAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return product.ToViewModel(_mapper);
        }

        public async Task<ProductViewModel> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            
            return product.ToViewModel(_mapper);
        }

        public async Task<ProductViewModel> UpdateProductAsync(ProductViewModel productViewModel)
        {
            var product = productViewModel.ToModel(_mapper);
            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();
            return product.ToViewModel(_mapper);
        }

        public async Task DeleteAsync(Guid id)
        {
            _productRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
