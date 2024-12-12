
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PointOfSale.DataBase.AppDbContextModels;
using PointOfSale.Domain.Models;
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

        //public async Task<Result<ResultSaleDetailModel>> CreateSaleDetailAsync(TblSaleDetail saleDetail)
        //{
        //    try
        //    {
        //        Result<ResultSaleDetailModel> model = new Result<ResultSaleDetailModel>();

        //        var voucherNumber = _db.TblSales.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == saleDetail.VoucherNo);
        //        var productCode = _db.TblSale.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCode == saleDetail.ProductCode);
        //        var voucherSaleDetails = _db.TblSaleDetails.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == saleDetail.VoucherNo);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Result<ResultSaleDetailModel>.SystemError(ex.Message);

        //    }
        //}
    }
}
