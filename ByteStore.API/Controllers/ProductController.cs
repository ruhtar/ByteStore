using System.Web.Http;
using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain.Entities;
using ByteStore.Shared.DTO;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;

namespace ByteStore.API.Controllers;

[Microsoft.AspNetCore.Mvc.Route("products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ITokenService _tokenService;

    public ProductController(IProductService productService, ITokenService tokenService)
    {
        _productService = productService;
        _tokenService = tokenService;
    }

    [Microsoft.AspNetCore.Mvc.HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _productService.GetAllProducts();
        if (products.ToList().Count == 0) return NoContent();
        return Ok(products);
    }

    [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productService.GetProductById(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [Microsoft.AspNetCore.Mvc.HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromForm] ProductDto productDto)
    {
        if (productDto.Price <= 0) return BadRequest("Invalid price");
        if (productDto.ProductQuantity <= 0) return BadRequest("Invalid quantity");
        productDto.ImageStorageUrl = await GetImageUrl(productDto);
        var newProduct = await _productService.AddProduct(productDto);
        return CreatedAtAction(nameof(GetProduct), new { id = newProduct.ProductId }, newProduct);
    }

    [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, [FromForm] UpdateProductDto productDto)
    {
        if (productDto.Price <= 0) return BadRequest("Invalid price");
        if (productDto.ProductQuantity <= 0) return BadRequest("Invalid quantity");
        var updatedProduct = await _productService.UpdateProduct(id, productDto);
        if (updatedProduct == false) return NotFound();
        return Ok();
    }

    [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var sucess = await _productService.DeleteProduct(id);
        if (!sucess) return NotFound();
        return NoContent();
    }

    [Authorize]
    public async Task<IActionResult> CreateReview(ReviewDto reviewDto)
    {
        if (!IsTokenValid()) return Unauthorized();
        await _productService.CreateReview(reviewDto);
        return Ok();
    }

    private static async Task<string?> GetImageUrl(ProductDto productDto)
    {
        return await new FirebaseStorage(Environment.GetEnvironmentVariable("FIREBASE_BUCKET"))
            .Child("ByteStore")
            .Child(productDto.Name)
            .PutAsync(productDto.Image.OpenReadStream());
    }
    
    private bool IsTokenValid()
    {
        string token = HttpContext.Request.Headers["Authorization"];

        if (string.IsNullOrEmpty(token)) return false;

        // Remove o prefixo "Bearer " do token, se presente
        token = token.Replace("Bearer ", "");

        return _tokenService.ValidateToken(token);
    }
}