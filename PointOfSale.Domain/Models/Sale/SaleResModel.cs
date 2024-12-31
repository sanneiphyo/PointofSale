namespace PointOfSale.Domain.Models.Sale;
public class ResultSaleModel
{
    public string VoucherNo { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<SaleItem> SaleItems { get; set; }

    public class SaleItem
    {
        public string ProductCode { get; set; }
        //public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
