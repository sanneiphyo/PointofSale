namespace PointofSale.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductCategoryController : ControllerBase
{

    public readonly ProductCategoryService _service;

    public ProductCategoryController(ProductCategoryService service)
    {
        _service = service;
    }

    #region CreateProductCategoryAsync

    [HttpPost("Create")]
    public async Task<IActionResult> CreateProductCategoryAsync([FromBody] ProductCategoryReqModel reqModel)
    {
        try
        {
            var result = await _service.CreateProductCategoryAsync(reqModel);
            return Ok(result);
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { error = ex.Message });
        }
    }

    #endregion

    #region GetProductCategoryAsync

    [HttpGet("Get")]
    public async Task<IActionResult> GetProductCategoryAsync()
    {
        try
        {
            var lst = await _service.GetProductCategoryAsync();
            return Ok(lst);
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { error = ex.Message });
        }
    }

    #endregion

    #region UpdateProductCategoryAsync


    [HttpPut("{productCategoryCode}")]
    public async Task<IActionResult> UpdateProductCategoryAsync(string productCategoryCode, ProductCategoryResModel resModel)
    {
        try
        {
            var item = await _service.UpdateProductCategoryAsync(productCategoryCode, resModel);
            return Ok(item);
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { error = ex.Message });
        }
    }

    #endregion

}
