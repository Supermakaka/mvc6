using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Discount { get; set; }
         
        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
