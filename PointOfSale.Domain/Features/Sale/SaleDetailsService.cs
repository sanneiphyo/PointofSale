
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PointOfSale.DataBase.AppDbContextModels;
using PointOfSale.Domain.Models;
using PointOfSale.Domain.Models.Product;
using PointOfSale.Domain.Models.Sale;

namespace PointOfSale.Domain.Features.Sale
{
    public class SaleDetailsService
    {
        private readonly AppDbContext _db;

        public SaleDetailsService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<ResultSaleDetailModel>> GetSaleDetailAsync(string voucherNo)
        {
            try
            {
                Result<ResultSaleDetailModel> model = new Result<ResultSaleDetailModel>();

                var SaleDetails = await _db.TblSaleDetails.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == voucherNo);

                if (string.IsNullOrEmpty(SaleDetails.VoucherNo))
                {
                    model = Result<ResultSaleDetailModel>.SystemError("Voucher does not exist");
                    goto Result;
                }

                var responseModel = new ResultSaleDetailModel
                {
                    Detail = SaleDetails
                };

                model = Result<ResultSaleDetailModel>.Success(responseModel, "Product retrieved successfully.");

            Result:
                return model;
            }
            catch (Exception ex)
            {
                return Result<ResultSaleDetailModel>.SystemError(ex.Message);

            }
        }

        public async Task<Result<ResultSaleDetailModel>> CreateSaleDetailAsync(TblSaleDetail saleDetail)
        {
            try
            {
                Result<ResultSaleDetailModel> model = new Result<ResultSaleDetailModel>();

                var voucherNumber = await _db.TblSales.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == saleDetail.VoucherNo);
                var productCode =await _db.TblProducts.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCode == saleDetail.ProductCode);
                var voucherSaleDetails =await _db.TblSaleDetails.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == saleDetail.VoucherNo);

                if (voucherNumber is null)
                {
                     model = Result<ResultSaleDetailModel>.ValidationError("Voucher No doesn't exist in Sale.");
                    goto Result;
                }
                if (productCode is null)
                {
                    model = Result<ResultSaleDetailModel>.ValidationError("Product Code doesn't exist. Please Create Product Code first!");
                    goto Result;
                }
                if (voucherSaleDetails != null)
                {
                     Result<ResultSaleDetailModel>.ValidationError("Voucher is already exist.");
                    goto Result;
                }
                if (!decimal.TryParse(saleDetail.Quantity, out var quantity))
                {
                    model = Result<ResultSaleDetailModel>.ValidationError("Invalid quantity value.");
                    goto Result;
                }

                voucherNumber.TotalAmount = productCode.Price * quantity;
                saleDetail.Price = productCode.Price;  

                _db.TblSales.Update(voucherNumber);
                await _db.TblSaleDetails.AddAsync(saleDetail);
                await _db.SaveChangesAsync();

                var responseModel = new ResultSaleDetailModel
                {
                    Detail = saleDetail
                };

                model = Result<ResultSaleDetailModel>.Success(responseModel, "Product retrieved successfully.");

            Result:
                return model;

            }
            catch (Exception ex)
            {
                return Result<ResultSaleDetailModel>.SystemError(ex.Message);

            }
        }
    }
}
