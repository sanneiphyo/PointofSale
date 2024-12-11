using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Domain.Features.Product;
using PointOfSale.Domain.Models.Product;

namespace PointofSale.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromBody] ProductResponseModel response)
    {
        try
        {
            var result = await _service.CreateProductAsync(response);
            return Ok(result);
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { error = ex.Message });
        }
    }
}
