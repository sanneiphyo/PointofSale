﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.DataBase.AppDbContextModels;

namespace PointOfSale.Domain.Models.ProductCategory
{
    public class ResultProductCategoryResponseModel
    {
        public TblProductCategory? ProductCategory { get; set; }
    }
}

