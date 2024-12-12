namespace PointOfSale.Domain.Models.Product;

public class ProductResponseModel
{
    public string ProductCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string ProductCategoryCode { get; set; } = null!;
}
