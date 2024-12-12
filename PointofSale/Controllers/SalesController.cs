using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Domain.Features.Sale;

namespace PointofSale.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SaleService _saleService;
        private readonly SaleDetailsService _saleDetailsService;

        public SalesController(SaleService saleService, SaleDetailsService saleDetailsService)
        {
            _saleService = saleService;
            _saleDetailsService = saleDetailsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSale(string voucherNo)
        {
            try
            {
                var lst = await _saleService.GetSaleAsync(voucherNo);
                var saleDetailResult = await _saleDetailsService.GetSaleDetailAsync(voucherNo);

                return Ok(lst);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
