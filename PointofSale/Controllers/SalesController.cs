using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.DataBase.AppDbContextModels;
using PointOfSale.Domain.Features.Sale;
using PointOfSale.Domain.Models.Sale;

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

        [HttpGet("get-sale-by-voucher")]
        public async Task<IActionResult> GetSale(string voucherNo)
        {
            try
            {
                var lst = await _saleService.GetSaleAsync(voucherNo);
                var saleDetailResult = await _saleDetailsService.GetSaleDetailAsync(voucherNo);

                return Ok(new
                {
                    SaleDetail = lst,
                    SaleDetailResult = saleDetailResult
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("sale")]
        public async Task<IActionResult> CreateSale(ResultSaleModel sale)
        {
            try
            {

               
                var Sale = await _saleService.CreateSaleAsync(sale);

              return Ok(Sale);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { error = ex.Message });
            }
        }

    }
}
