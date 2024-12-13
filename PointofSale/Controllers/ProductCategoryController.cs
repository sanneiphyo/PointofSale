using PointOfSale.Domain.Features.ProductCategory;
using PointOfSale.Domain.Models.ProductCategory;

namespace PointofSale.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {

        public readonly ProductCategoryService _service;

        public ProductCategoryController(ProductCategoryService service)
        {
            _service = service;
        }
        

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

  


    }
}
