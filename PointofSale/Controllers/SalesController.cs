using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.DataBase.AppDbContextModels;
using PointOfSale.Domain.Models.Sale;

[Route("api/[controller]")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly SaleService _saleService;


    public SalesController(SaleService saleService)
    {
        _saleService = saleService;

    }

    #region GetSale

        [HttpGet("get-sale-by-voucher")]
        public async Task<IActionResult> GetSale(string voucherNo)
        {
            try
            {
                var lst = await _saleService.GetSaleAsync(voucherNo);
         
               return Ok(lst);
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
#endregion