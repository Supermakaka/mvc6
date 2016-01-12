﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class SubCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Discount { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
