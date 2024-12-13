namespace PointOfSale.Domain.Features.Sale;

public  class SaleService
{
    private readonly AppDbContext _db;
    private readonly SaleDetailsService _saleDetailsService;

    public SaleService(AppDbContext db , SaleDetailsService saleDetailsService)
    {
        _db = db;
        _saleDetailsService = saleDetailsService;
    }

    #region GetSaleAsync

    public async Task<Result<ResultSaleModel>> GetSaleAsync( string voucherNo)
    {
        Result<ResultSaleModel> model = new Result<ResultSaleModel>();
        try
        {
            var VoucherNumber = await _db.TblSales.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == voucherNo);
            var saleDetail = await _saleDetailsService.GetSaleDetailAsync(voucherNo);


            if (string.IsNullOrEmpty(VoucherNumber.VoucherNo))
            {
                model = Result<ResultSaleModel>.ValidationError("Voucher Number does not exist");
                goto Result;
            }

            var responseModel = new ResultSaleModel
            {
                Sale = VoucherNumber
            };

            model = Result<ResultSaleModel>.Success(responseModel, "Product retrieved successfully.");

        Result:
            return model;

        }
        catch (Exception ex)
        {

            return Result<ResultSaleModel>.SystemError(ex.Message);
        }
    }

    #endregion

    public async Task<Result<ResultSaleModel>> CreateSaleAsync(TblSale sale)
    {
        try
        {
            Result<ResultSaleModel> model = new Result<ResultSaleModel>();

            var VoucherNumber = _db.TblSales.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == sale.VoucherNo);

            if (string.IsNullOrEmpty(sale.VoucherNo))
            {
                model = Result<ResultSaleModel>.ValidationError("You need to put voucher number");
                goto Result;
            }

            await _db.TblSales.AddAsync(sale);
            await _db.SaveChangesAsync();

            var responseModel = new ResultSaleModel
            {
                Sale = sale,
            };

            model = Result<ResultSaleModel>.Success(responseModel, "Product retrieved successfully.");

            Result:
            return model;
        }
        catch (Exception ex)
        {
            return Result<ResultSaleModel>.SystemError(ex.Message);

        }
    }
}

