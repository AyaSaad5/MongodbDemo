using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.DTOs;
using MongoDB.Entities;
using MongoDB.Repositories;

namespace MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<List<ProductDetails>> GetAllProducts()
        {
            return await _productService.listProductsAsync();
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDetails>> GetProductById(string productId)
        {
            return await _productService.GetProductByIdAsync(productId);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDetails product)
        {
            await _productService.AddProductAsync(product);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync(string productId, updateProductDTO product)
        {
            var product_ = await _productService.GetProductByIdAsync(productId);
            if(product_ == null)
            {
                return NotFound();
            }
             await _productService.UpdateProductAsync(productId, product);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductAsync(string productId)
        {
            var product_ = await _productService.GetProductByIdAsync(productId);
            if (product_ == null)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(productId);
            return Ok();
        }

    }
}
