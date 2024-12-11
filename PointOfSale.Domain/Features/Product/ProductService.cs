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
            Result<ProductResponseModel> model;

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

        #region  GetProductAsync

        public async Task<Result<List<ProductModel>>> GetProductAsync()
        {
            Result<List<ProductModel>> model;

            try
            {
                var product = _db.TblProducts
                    .Where(x => x.DeleteFlag == false)
                    .AsNoTracking();

                var item = await product.Select(x => new ProductModel()
                {
                    Id = x.Id,
                    ProductCode = x.ProductCode,
                    Name = x.Name,
                    Price = x.Price,
                    ProductCategoryCode = x.ProductCategoryCode,
                    DeleteFlag = x.DeleteFlag
                }).ToListAsync();

                return Result<List<ProductModel>>.Success(item);
            }
            catch(Exception ex)
            {
                return Result<List<ProductModel>>.SystemError(ex.Message);
            }
        }

        #endregion

        #region UpdateProductAsync

        public async Task<Result<ProductRequestModel>> UpdateProductAsync(string productCode, ProductRequestModel requestModel)
        {
            Result<ProductRequestModel> model;

            try
            {
                var product = await _db.TblProducts.FirstOrDefaultAsync(x => x.ProductCode == productCode);

                if(product is null)
                {
                    model = Result<ProductRequestModel>.SystemError("No Data Found with this Product Code");
                    return model;
                }

                product.Price = requestModel.Price;

                _db.TblProducts.Update(product);
                await _db.SaveChangesAsync();

                model = Result<ProductRequestModel>.Success(requestModel);

                return model;
            }
            catch (Exception ex)
            {
                return Result<ProductRequestModel>.SystemError(ex.Message);
            }
        }

        #endregion

    }
}

