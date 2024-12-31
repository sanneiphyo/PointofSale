namespace PointOfSale.Domain.Models.Sale;

public class SaleRequestMode
{
    public int SaleId { get; set; }

    public string VoucherNo { get; set; } = null!;

    public DateTime? SaleDate { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual ICollection<TblSaleDetail> TblSaleDetails { get; set; } = new List<TblSaleDetail>();


}
