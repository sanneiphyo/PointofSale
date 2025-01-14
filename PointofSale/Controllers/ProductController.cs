﻿namespace PointofSale.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }

    #region CreateProductAsync

    [HttpPost("Create")]
    public async Task<IActionResult> CreateProductAsync([FromBody] ProductResponseModel response)
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

    #endregion

    #region GetProductAsync

    [HttpGet("Get")]
    public async Task<IActionResult> GetProductAsync()
    {
        try
        {
            var lst = await _service.GetProductAsync();
            return Ok(lst);
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { error = ex.Message });
        }
    }

    #endregion

    #region UpdateProductAsync

    [HttpPut("{productCode}")]
    public async Task<IActionResult>UpdateProductAsync(string productCode, ProductRequestModel requestModel)
    {
        try
        {
            var item = await _service.UpdateProductAsync(productCode, requestModel);
            return Ok(item);
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { error = ex.Message });
        }
    }

    #endregion

    #region SoftDeleteProductAsync

    [HttpDelete("{productCode}")]
    public async Task<IActionResult> SoftDeleteProductAsync(string productCode)
    {
        try
        {
            var item = await _service.SoftDeleteProductAsync(productCode);
            return Ok(item);
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { error = ex.Message });
        }
    }

    #endregion

}
