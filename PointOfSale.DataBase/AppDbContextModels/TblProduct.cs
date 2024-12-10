using System;
using System.Collections.Generic;

namespace PointOfSale.DataBase.AppDbContextModels;

public partial class TblProduct
{
    public int Id { get; set; }

    public string ProductCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string ProductCategoryCode { get; set; } = null!;

    public bool? DeleteFlag { get; set; }

    public virtual TblProductCategory ProductCategoryCodeNavigation { get; set; } = null!;
}
