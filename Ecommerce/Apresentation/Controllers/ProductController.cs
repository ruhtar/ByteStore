using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Host.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProducts();
            if (products.ToList().Count == 0) return NoContent();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] RequestProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                ProductQuantity = productDto.ProductQuantity,
                Price = productDto.Price,
            };
            var newProduct = await _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.ProductId }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] RequestProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                ProductQuantity = productDto.ProductQuantity,
                Price = productDto.Price,
            };
            var updatedProduct = await _productService.UpdateProduct(id, product);
            if (updatedProduct == null) return NotFound();
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var sucess = await _productService.DeleteProduct(id);
            if (!sucess) return NotFound();
            return NoContent();
        }
    }
}
