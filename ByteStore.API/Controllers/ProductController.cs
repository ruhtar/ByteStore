using ByteStore.API.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain.Entities;
using ByteStore.Shared.DTO;
using Firebase.Storage;

namespace ByteStore.API.Controllers
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
        public async Task<ActionResult<PagedDto<Product>>> GetProducts([FromQuery] GetProductsInputPagination input)
        {
            var products = await _productService.GetAllProducts(input);
            var pagedDto = new PagedDto<Product>(products);
            if (products.Count == 0) return NoContent();
            return Ok(pagedDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [Authorize]
        [TokenValidation]
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromForm] ProductDto productDto)
        {
            if (productDto.Price <= 0) return BadRequest("Invalid price");
            if (productDto.ProductQuantity <= 0) return BadRequest("Invalid quantity");
            productDto.ImageStorageUrl = await GetImageUrl(productDto);
            var newProduct = await _productService.AddProduct(productDto);
            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.ProductId }, newProduct);
        }
        
        [Authorize]
        [TokenValidation]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromForm] UpdateProductDto productDto)
        {
            if (productDto.Price <= 0) return BadRequest("Invalid price");
            if (productDto.ProductQuantity <= 0) return BadRequest("Invalid quantity");
            var updatedProduct = await _productService.UpdateProduct(id, productDto);
            if (updatedProduct == false) return NotFound();
            return NoContent(); // Use NoContent for successful updates
        }

        [Authorize]
        [TokenValidation]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var success = await _productService.DeleteProduct(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [Authorize]
        [TokenValidation]
        [HttpPost("reviews")]
        public async Task<IActionResult> CreateReview(ReviewDto reviewDto)
        {
            var review = await _productService.CreateReview(reviewDto);
            return review == null ? Problem("Something went wrong by creating the review.") : Ok(review);
        }

        [HttpGet("reviews")]
        public async Task<IActionResult> GetReviews([FromQuery] int productId)
        {
            var reviews = await _productService.GetReviewsByProductId(productId);
            return reviews == null ? Problem("Something went wrong by retrieving the reviews.") : Ok(reviews);
        }

        private static async Task<string?> GetImageUrl(ProductDto productDto)
        {
            return await new FirebaseStorage(Environment.GetEnvironmentVariable("FIREBASE_BUCKET"))
                .Child("ByteStore")
                .Child(productDto.Name)
                .PutAsync(productDto.Image.OpenReadStream());
        }
    }
}
