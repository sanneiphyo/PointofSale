using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Domain.Models.ProductCategory
{
    public class ProductCategoryReqModel
    {
        public string ProductCategoryCode { get; set; } = null!;

        public string Name { get; set; } = null!;

        public decimal DeleteFlag { get; set; } = 0; 
    }
}
