namespace PointOfSale.DataBase.AppDbContextModels;

#region TblProductCategory

public partial class TblProductCategory
{
    public int ProductCategoryId { get; set; }

    public string ProductCategoryCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool? DeleteFlag { get; set; }

    public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}

#endregion