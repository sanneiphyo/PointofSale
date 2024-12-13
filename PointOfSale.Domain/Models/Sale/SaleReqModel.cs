using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.DataBase.AppDbContextModels;

namespace PointOfSale.Domain.Models.Sale
{

    public class SaleRequestModel
    {
        public TblSale? Sale { get; set; }

        public TblSaleDetail? SaleDetail { get; set; }
    }
}
