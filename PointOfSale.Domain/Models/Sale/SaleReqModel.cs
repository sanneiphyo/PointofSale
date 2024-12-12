using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Domain.Models.Sale
{
    public class SaleReqModel
    {
        public int SaleId { get; set; }

        public string VoucherNo { get; set; } = null!;

        public DateTime? SaleDate { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
