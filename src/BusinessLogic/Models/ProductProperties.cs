﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class ProductProperties
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UnitId { get; set; }
        public Unit Unit { get; set; }

        public string Value { get; set; }

        public int? ProductCategoryID { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

        public int? ProductSubCategoryID { get; set; }
        public virtual ProductSubCategory ProductSubCategory { get; set; }        
    }
}