using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public bool Deleted { get; set; }

        public bool Enabled { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public virtual ICollection<ProductSubCategory> ProductSubCategories { get; set; }
    }
}
