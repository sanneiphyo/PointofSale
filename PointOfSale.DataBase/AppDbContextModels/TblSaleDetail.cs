namespace PointOfSale.DataBase.AppDbContextModels;

#region TblSaleDetail

public partial class TblSaleDetail
{
    public int SaleDetailId { get; set; }

    public string VoucherNo { get; set; } = null!;

    public string ProductCode { get; set; } = null!;

    public string Quantity { get; set; } = null!;

    public decimal? Price { get; set; }

    public virtual TblSale VoucherNoNavigation { get; set; } = null!;
}

#endregion
