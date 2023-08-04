using AuthenticationService.Application.Services;
using AuthenticationService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Host.Controllers
{
    [Route("product/redis")]
    [ApiController]
    public class ProductRedisController : ControllerBase
    {
        private readonly IRedisProductService _productService;

        public ProductRedisController(IRedisProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            var newProduct = await _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var updatedProduct = await _productService.UpdateProduct(product);
            if (updatedProduct == null)
            {
                return NotFound();
            }

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
