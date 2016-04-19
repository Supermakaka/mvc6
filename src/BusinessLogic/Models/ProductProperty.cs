using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class ProductProperty
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        public string Value { get; set; }

        public bool Deleted { get; set; }

        public bool Enabled { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        //We can add proerties to whole category
        public int? ProductCategoryID { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

        //We can add proerties to each subcategory individualy
        public int? ProductSubCategoryID { get; set; }
        public virtual ProductSubCategory ProductSubCategory { get; set; }      
    }
}
