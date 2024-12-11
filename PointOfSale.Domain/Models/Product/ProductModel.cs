using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Domain.Models.Product
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string ProductCode { get; set; } = null!;

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string ProductCategoryCode { get; set; } = null!;

        public bool? DeleteFlag { get; set; }

    }
}
