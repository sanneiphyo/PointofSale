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

        public async Task<Result<ProductReqModel>> CreateProduct(string productCode ,string name,decimal price)
        {
            Result <ProductResponseModel> model = new Result<ProductResponseModel> ();

            var ExistingProduct = _db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCode == productCode);

            if (ExistingProduct is null) 
            { 
                model = Result<ProductResponseModel>.SystemError("Product Code is already exist");
                goto Result;
            }

            if (productCode.Length > 4)
            {

                model = Result<ProductResponseModel>.SystemError("character must be 4 letters ");
                goto Result;
            }

            var newProduct = new Product
            {
                ProductCode = productCode,
                Name = name,
                Price = price

            };

            await _db.Product.AddAsync(newProduct);
            await _db.SaveChangesAsync();


            var item = new ProductResponseModel
            {
                Prduct = newProduct
            };

            model = Result<productResponseModel>.Success(item, "Success.");

  
        }
        Result:
            return model;
    }

}

