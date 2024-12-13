namespace PointOfSale.Domain.Features.ProductCategory;

public class ProductCategoryService
{
    private readonly AppDbContext _db;

    public ProductCategoryService(AppDbContext db)
    {
        _db = db;
    }

    #region CreateProductCategoryAsync

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

    #endregion

    #region GetProductCategoryAsync

    public async Task<Result<List<ProductCategoryModel>>> GetProductCategoryAsync()
    {
        Result<List<ProductCategoryModel>> model;

        try
        {
            var productCategory = _db.TblProductCategories
                .Where(x => x.DeleteFlag == false)
                .AsNoTracking();

            var item = await productCategory.Select(x => new ProductCategoryModel()
            {
                ProductCategoryId = x.ProductCategoryId,
                ProductCategoryCode = x.ProductCategoryCode,
                Name = x.Name,
                DeleteFlag = false
               
            }).ToListAsync();

            return Result<List<ProductCategoryModel>>.Success(item);
        }
        catch (Exception ex)
        {
            return Result<List<ProductCategoryModel>>.SystemError(ex.Message);
        }
    }

    #endregion

    public async Task<Result<ProductCategoryResModel>> UpdateProductCategoryAsync(string productCategoryCode, ProductCategoryResModel resModel)
    {
        Result<ProductCategoryResModel> model;

        try
        {
            var productCategory = await _db.TblProductCategories.FirstOrDefaultAsync(x => x.ProductCategoryCode == productCategoryCode);

            if (productCategory is null)
            {
                model = Result<ProductCategoryResModel>.SystemError("No Data Found with this Product Code");
                return model;
            }

            productCategory.Name = resModel.Name;

            _db.TblProductCategories.Update(productCategory);
            await _db.SaveChangesAsync();

            model = Result<ProductCategoryResModel>.Success(resModel);

            return model;
        }
        catch (Exception ex)
        {
            return Result<ProductCategoryResModel>.SystemError(ex.Message);
        }
    }
}

