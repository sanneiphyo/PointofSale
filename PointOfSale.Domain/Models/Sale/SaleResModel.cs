namespace PointOfSale.Domain.Models.Sale;

namespace PointOfSale.Domain.Models.Sale
{
   public class ResultSaleModel
    {
        public int SaleId { get; set; }

        public string VoucherNo { get; set; } = null!;

        public DateTime? SaleDate { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual ICollection<TblSaleDetail> TblSaleDetails { get; set; } = new List<TblSaleDetail>();


    }

    public class ResultSaleDetailModel
    {
       public int SaleDetailId { get; set; }

    public string VoucherNo { get; set; } = null!;

    public string ProductCode { get; set; } = null!;

    public string Quantity { get; set; } = null!;

    public decimal? Price { get; set; }

    public virtual TblSale VoucherNoNavigation { get; set; } = null!;
}

#endregion
