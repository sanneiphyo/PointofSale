using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.DataBase.AppDbContextModels;

namespace PointOfSale.Domain.Models.Product
{
    public class ProductReqModel
    {
        public string ProductCode { get; set; } = null!;

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

       

    }
}
