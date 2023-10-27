using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain.Entities;
using ByteStore.Shared.DTO;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;

namespace ByteStore.API.Controllers;

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
    public async Task<ActionResult<Product>> CreateProduct([FromForm] ProductDto productDto)
    {
        if (productDto.Price <= 0) return BadRequest("Invalid price");
        if (productDto.ProductQuantity <= 0) return BadRequest("Invalid quantity");
        productDto.ImageStorageUrl = await GetImageUrl(productDto);
        var newProduct = await _productService.AddProduct(productDto);
        return CreatedAtAction(nameof(GetProduct), new { id = newProduct.ProductId }, newProduct);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] ProductDto productDto)
    {
        if (productDto.Price <= 0) return BadRequest("Invalid price");
        if (productDto.ProductQuantity <= 0) return BadRequest("Invalid quantity");
        var updatedProduct = await _productService.UpdateProduct(id, productDto);
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

    private static async Task<string?> GetImageUrl(ProductDto productDto)
    {
        return await new FirebaseStorage(Environment.GetEnvironmentVariable("FIREBASE_BUCKET"))
            .Child("ByteStore")
            .Child(productDto.Name)
            .PutAsync(productDto.Image.OpenReadStream());
    }
}