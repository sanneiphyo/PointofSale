using Microsoft.EntityFrameworkCore;
using PointOfSale.DataBase.AppDbContextModels;
using PointOfSale.Domain.Models;
using PointOfSale.Domain.Models.ProductCategory;

namespace PointOfSale.Domain.Features.ProductCategory
{
    public class ProductCategoryService
    {
        private readonly AppDbContext _db;

        public ProductCategoryService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<ProductCategoryResponseModel>> CreateProductCategoryAsync(ProductCategoryReqModel response)
        {
            Result<ProductCategoryResponseModel> model = new Result<ProductCategoryResponseModel>();

            try
            {
                //check ProductCategoryCode
                var existingCategory = await _db.TblProductCategories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ProductCategoryCode == response.ProductCategoryCode);

                if (existingCategory != null)
                {
                    model = Result<ProductCategoryResponseModel>.SystemError("Product Category Code already exists");
                    return model;
                }

                //check code length
                if (response.ProductCategoryCode.Length != 4)
                {
                    model = Result<ProductCategoryResponseModel>.SystemError("Product Category Code must be exactly 4 characters");
                    return model;
                }

                //Create 
                var productCategory = new TblProductCategory
                {
                    ProductCategoryCode = response.ProductCategoryCode,
                    Name = response.Name,
                    DeleteFlag = response.DeleteFlag
                };

                //update
                await _db.TblProductCategories.AddAsync(productCategory);
                await _db.SaveChangesAsync();

                model = Result<ProductCategoryResponseModel>.Success(new ProductCategoryResponseModel
                {
                    ProductCategoryCode = productCategory.ProductCategoryCode,
                    Name = productCategory.Name,
                    DeleteFlag = productCategory.DeleteFlag
                });

                return model;
            }
            catch (Exception ex)
            {
                return Result<ProductCategoryResponseModel>.SystemError(ex.Message);
            }
        }
    }
}

