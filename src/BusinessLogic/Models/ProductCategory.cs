using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public virtual ICollection<ProductSubCategory> ProductSubCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
