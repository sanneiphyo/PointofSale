
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
                    VoucherNo = voucherNo,
                    ProductCode = SaleDetails.ProductCode,
                    Quantity = SaleDetails.Quantity,
                    Price = SaleDetails.Price,

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


        public async Task<Result<ResultSaleDetailModel>> CreateSaleDetailAsync(ResultSaleDetailModel saleDetail)
        {
            try
            {

                var voucher = await _db.TblSales.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == saleDetail.VoucherNo);


                var product = await _db.TblProducts.AsNoTracking()
                   .FirstOrDefaultAsync(x => x.ProductCode == saleDetail.ProductCode);


                var existingSaleDetail = await _db.TblSaleDetails.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.VoucherNo == saleDetail.VoucherNo && x.ProductCode == saleDetail.ProductCode);


                if (voucher == null)
                {
                    return Result<ResultSaleDetailModel>.ValidationError("Voucher No doesn't exist in Sale.");
                }

                if (product == null)
                {
                    return Result<ResultSaleDetailModel>.ValidationError("Product Code doesn't exist. Please create Product Code first!");
                }

                if (existingSaleDetail != null)
                {
                    return Result<ResultSaleDetailModel>.ValidationError("Sale detail for this Voucher and Product already exists.");
                }

                if (!decimal.TryParse(saleDetail.Quantity, out var quantity))
                {
                    return Result<ResultSaleDetailModel>.ValidationError("Invalid quantity value.");
                }


                var totalAmount = product.Price * quantity;
                saleDetail.Price = product.Price;


                voucher.TotalAmount += totalAmount;

                var saleDetailEntity = new TblSaleDetail
                {
                    VoucherNo = saleDetail.VoucherNo,
                    ProductCode = saleDetail.ProductCode,
                    Quantity = quantity.ToString(),
                    Price = saleDetail.Price,

                };


                await _db.TblSaleDetails.AddAsync(saleDetailEntity);
                _db.TblSales.Update(voucher);
                await _db.SaveChangesAsync();


                var responseModel = new ResultSaleDetailModel
                {
                    VoucherNo = saleDetailEntity.VoucherNo,
                    ProductCode = saleDetailEntity.ProductCode,
                    Quantity = quantity.ToString(),
                    Price = saleDetailEntity.Price
                };

                return Result<ResultSaleDetailModel>.Success(responseModel, "Sale detail created successfully.");
            }
            catch (Exception ex)
            {
                return Result<ResultSaleDetailModel>.SystemError($"An error occurred: {ex.Message}");
            }
        }


    
    }
}
