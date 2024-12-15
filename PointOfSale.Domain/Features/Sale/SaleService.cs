

using PointOfSale.DataBase.AppDbContextModels;

using PointOfSale.Domain.Models.Sale;
using static PointOfSale.Domain.Models.Sale.ResultSaleModel;

public class SaleService
{
    private readonly AppDbContext _db;

    public SaleService(AppDbContext db)
    {
        _db = db;
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

            var responseModel = new ResultSaleModel
            {
                VoucherNo = sale.VoucherNo,
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

    public async Task<Result<ResultSaleModel>> CreateSaleAsync(ResultSaleModel request)
    {
        Result<ResultSaleModel> model = new Result<ResultSaleModel>();
        var totalAmount = 0m;


        var sale = new TblSale
        {
            VoucherNo = request.VoucherNo,
            SaleDate = DateTime.UtcNow,
        };

        await _db.TblSales.AddAsync(sale);
        await _db.SaveChangesAsync();

        var saleItems = new List<SaleItem>();

        foreach (var item in request.SaleItems)
        {
            var product = await _db.TblProducts
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync(x => x.ProductCode == item.ProductCode );

            if (product is null)
            {
                model = Result<ResultSaleModel>.ValidationError("Product does not exist");
                goto Result;
            }

            var saleDetail = new TblSaleDetail
            {
                VoucherNo = request.VoucherNo,
                ProductCode = item.ProductCode,
                Quantity = item.Quantity.ToString(),
                Price = product.Price
            };
            await _db.TblSaleDetails.AddAsync(saleDetail);

            totalAmount += product.Price * item.Quantity;

            saleItems.Add(new ResultSaleModel.SaleItem
            {
                ProductCode = product.ProductCode,
                Quantity = item.Quantity,
                Price = product.Price
            });
        }
        await _db.SaveChangesAsync();

        sale.TotalAmount = totalAmount;
        _db.TblSales.Update(sale);
        await _db.SaveChangesAsync();

        var response = new ResultSaleModel
        {
            VoucherNo = sale.VoucherNo,
            SaleDate = (DateTime)sale.SaleDate,
            TotalAmount = sale.TotalAmount,
            SaleItems = saleItems
        };

        model = Result<ResultSaleModel>.Success(response, "Sale created successfully.");

    Result:
        return model;
    }
}

   



