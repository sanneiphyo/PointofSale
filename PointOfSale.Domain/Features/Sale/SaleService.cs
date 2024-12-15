

using PointOfSale.Domain.Features.Sale;
using PointOfSale.Domain.Models.Sale;

public class SaleService
{
    private readonly AppDbContext _db;
    private readonly SaleDetailsService _saleDetailsService;

    public SaleService(AppDbContext db, SaleDetailsService saleDetailsService)
    {
        _db = db;
        _saleDetailsService = saleDetailsService;
    }

    public async Task<Result<ResultSaleModel>> GetSaleAsync(string voucherNo)
    {
        Result<ResultSaleModel> model = new Result<ResultSaleModel>();
        try
        {
        
            var sale = await _db.TblSales.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == voucherNo);

            if (sale == null)
            {
                model = Result<ResultSaleModel>.ValidationError("Voucher Number does not exist");
                goto Result;
            }

           
            var saleDetailsResult = await _saleDetailsService.GetSaleDetailAsync(voucherNo);

            if (!saleDetailsResult.IsSuccess)
            {
                model = Result<ResultSaleModel>.ValidationError("Error");
                goto Result;
            }

            var responseModel = new ResultSaleModel
            {
                VoucherNo = sale.VoucherNo,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
             
            };

            model = Result<ResultSaleModel>.Success(responseModel, "Sale retrieved successfully.");

        Result:
            return model;
        }
        catch (Exception ex)
        {
            return Result<ResultSaleModel>.SystemError(ex.Message);
        }
    }

    public async Task<Result<ResultSaleModel>> CreateSaleAsync(ResultSaleModel sale)
    {
        try
        {
            var existingSale = await _db.TblSales.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == sale.VoucherNo);

            if (existingSale != null)
            {
                return Result<ResultSaleModel>.ValidationError("A sale with this voucher number already exists.");
            }

         
            var newSale = new TblSale
            {
                VoucherNo = sale.VoucherNo,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount 
            };

        
            await _db.TblSales.AddAsync(newSale);
            await _db.SaveChangesAsync();

          
            var responseModel = new ResultSaleModel
            {
                VoucherNo = newSale.VoucherNo,
                SaleDate = newSale.SaleDate,
                TotalAmount = newSale.TotalAmount
            };

            return Result<ResultSaleModel>.Success(responseModel, "Sale created successfully.");
        }
        catch (Exception ex)
        {
            return Result<ResultSaleModel>.SystemError($"An error occurred: {ex.Message}");
        }
    }

   


}
