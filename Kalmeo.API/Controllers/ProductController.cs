using Kalmeo.Services.Interfaces;
using Kalmeo.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Kalmeo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpPost]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await _productService.CreateProductAsync(productViewModel);
            return Created($"/product/{productViewModel.Id}", productViewModel);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var productViewModel = await _productService.GetProductByIdAsync(id);
            if (productViewModel == null) return NotFound();
            return Ok(productViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid id, ProductViewModel productViewModel)
        {
            var exists = await _productService.IsProductExistsAsync(id);
            if (!exists) return NotFound();
            else if (id != productViewModel.Id) return Problem("Supplied product contains different id than provided.", null, StatusCodes.Status400BadRequest);
            else
            {
                productViewModel = await _productService.UpdateProductAsync(productViewModel);
                return Ok(productViewModel);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var exists = await _productService.IsProductExistsAsync(id);
            if (!exists) return NotFound();
            await _productService.DeleteAsync(id);
            return Ok(id);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var productViewModels = await _productService.GetAllAsync();
                if (productViewModels == null) return NotFound();
                return Ok(productViewModels);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }        
    }
}
