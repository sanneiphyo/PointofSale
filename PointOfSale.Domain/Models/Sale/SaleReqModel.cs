namespace PointOfSale.Domain.Models.Sale;

#region SaleRequestModel

public class SaleRequestModel
{
    public TblSale? Sale { get; set; }
    public TblSaleDetail? SaleDetail { get; set; }
}

#endregion
