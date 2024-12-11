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

        #region CreateProductAsync

        public async Task<Result<ProductResponseModel>> CreateProductAsync(ProductResponseModel response)
        {
            Result<ProductResponseModel> model = new Result<ProductResponseModel>();

            try
            {
                var existingProduct = await _db.TblProducts
                                               .AsNoTracking()
                                               .FirstOrDefaultAsync(x => x.ProductCode == response.ProductCode);

                if (existingProduct != null)
                {
                    model = Result<ProductResponseModel>.SystemError("Product Code already exists");
                    return model;
                }

                if (response.ProductCode.Length != 4)
                {
                    model = Result<ProductResponseModel>.SystemError("Product Code must be exactly 4 numeric characters");
                    return model;
                }

                var product = new TblProduct
                {
                    ProductCode = response.ProductCode,
                    Name = response.Name,
                    Price = response.Price,
                    ProductCategoryCode = response.ProductCategoryCode,
                };

                await _db.TblProducts.AddAsync(product);
                await _db.SaveChangesAsync();

                model = Result<ProductResponseModel>.Success(new ProductResponseModel
                {
                    ProductCode = product.ProductCode,
                    Name = product.Name,
                    Price = product.Price,
                    ProductCategoryCode = product.ProductCategoryCode
                });

                return model;
            }
            catch (Exception ex)
            {
                return Result<ProductResponseModel>.SystemError(ex.Message);
            }
        }

        #endregion

    }
}

