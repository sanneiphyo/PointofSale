using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PointOfSale.DataBase.AppDbContextModels;
using PointOfSale.Domain.Models;
using PointOfSale.Domain.Models.Product;

namespace PointOfSale.Domain.Features.Product
{
    public class ProductService
    {
        private readonly AppDbContext _db;

        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<ResultProductResponseModel>> CreateProductAsync(string productCode, string name, decimal price)
        {
            Result<ResultProductResponseModel> model = new Result<ResultProductResponseModel>();

            var ExistingProduct = _db.TblProducts.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCode == productCode);

            if (ExistingProduct is null)
            {
                model = Result<ResultProductResponseModel>.SystemError("Product Code is already exist");
                goto Result;
            }

            if (productCode.Length > 4)
            {

                model = Result<ResultProductResponseModel>.SystemError("character must be 4 letters ");
                goto Result;
            }

            var newProduct = new TblProduct
            {
                ProductCode = productCode,
                Name = name,
                Price = price

            };

            await _db.TblProducts.AddAsync(newProduct);
            await _db.SaveChangesAsync();


            var item = new ResultProductResponseModel
            {
                Product = newProduct
            };
            model = Result<ResultProductResponseModel>.Success(item, "Success.");

        Result:
            return model;
        }
    }
}

