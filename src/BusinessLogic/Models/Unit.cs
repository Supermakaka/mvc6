﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class Unit
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int UnitTypeId { get; set; }
        public virtual UnitType UnitType { get; set; } 

        public virtual ICollection<ProductProperties> ProductProperties { get; set; }
    }
}
