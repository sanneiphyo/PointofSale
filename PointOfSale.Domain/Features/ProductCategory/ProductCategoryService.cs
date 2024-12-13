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

      
        public async Task<Result<ProductCategoryReqModel>> CreateProductCategoryAsync(ProductCategoryReqModel response)
        {
            Result<ProductCategoryReqModel> model;

            try
            {
                var existingProductCategory = await _db.TblProductCategories
                                               .AsNoTracking()
                                               .FirstOrDefaultAsync(x => x.ProductCategoryCode == response.ProductCategoryCode);

                if (existingProductCategory is not null)
                {
                    model = Result<ProductCategoryReqModel>.SystemError("Product Code already exists");
                    return model;
                }

                if (response.ProductCategoryCode.Length != 4)
                {
                    model = Result<ProductCategoryReqModel>.SystemError("Product Code must be exactly 4 numeric characters");
                    return model;
                }

                var productCategory = new TblProductCategory
                {
                    ProductCategoryCode = response.ProductCategoryCode,
                    Name = response.Name,
                };

                await _db.TblProductCategories.AddAsync(productCategory);
                await _db.SaveChangesAsync();

                model = Result<ProductCategoryReqModel>.Success(new ProductCategoryReqModel
                {
                 
                    ProductCategoryCode = productCategory.ProductCategoryCode,
                    Name = productCategory.Name,
                });

                return model;
            }
            catch (Exception ex)
            {
                return Result<ProductCategoryReqModel>.SystemError(ex.Message);
            }
        }

       
    }
}

