namespace PointOfSale.Domain.Models.ProductCategory;

#region ProductCategoryModel

public class ProductCategoryModel
{
    public int ProductCategoryId { get; set; }

    public string ProductCategoryCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool? DeleteFlag { get; set; }
}

#endregion
