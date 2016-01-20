using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int ItemsCount { get; set; }

        public bool Deleted { get; set; }

        public bool Visible { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public int ProductSubCategoryId { get; set; }
        public virtual ProductSubCategory ProductSubCategory { get; set; } 
    }
}
