namespace PointofSale.RestApi.Controllers;

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

    #region GetSale

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

    #endregion

    //[HttpPost("sale")]
    //public async Task<IActionResult> CreateSale(SaleRequestModel reqModel)
    //{
    //    try
    //    {
    //        TblSale sale = reqModel.Sale!;
    //        TblSaleDetail saleDetail = reqModel.SaleDetail!;

    //        var item = await _saleService.CreateSaleAsync(sale);
    //        var result = await _saleDetailsService.CreateSaleDetailAsync(saleDetail);

    //        return Ok(result);
    //    }
    //    catch (Exception ex)
    //    {

    //        return StatusCode(500, new { error = ex.Message });
    //    }
    //}

    #region CreateSale

    [HttpPost("sale")]
    public async Task<IActionResult> CreateSale(SaleRequestModel reqModel)
    {
        try
        {
        
            TblSale sale = reqModel.Sale!;
            TblSaleDetail saleDetail = reqModel.SaleDetail!;

          
            var saleResult = await _saleService.CreateSaleAsync(sale);
          
            var saleDetailResult = await _saleDetailsService.CreateSaleDetailAsync(saleDetail);
         
            return Ok(new
            {
                Sale = saleResult.Data,
                SaleDetail = saleDetailResult.Data
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    #endregion
}
