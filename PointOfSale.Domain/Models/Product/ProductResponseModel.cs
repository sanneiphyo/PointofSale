using System;
using PointOfSale.DataBase.AppDbContextModels;

namespace PointOfSale.Domain.Models.ProductCategory
{
    public class ResultProductCategoryResponseModel
    {
       public TblProductCategory? ProductCategory { get; set; } = null!; //category ka null ma phyit buu htin dr pl please correct me if i'm wrong
    }
}
