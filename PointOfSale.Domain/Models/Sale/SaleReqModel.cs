using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.DataBase.AppDbContextModels;

namespace PointOfSale.Domain.Models.Sale
{
    public class SaleRequestMode
    {
        public int SaleId { get; set; }

        public string VoucherNo { get; set; } = null!;

        public DateTime? SaleDate { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual ICollection<TblSaleDetail> TblSaleDetails { get; set; } = new List<TblSaleDetail>();


    }
}
