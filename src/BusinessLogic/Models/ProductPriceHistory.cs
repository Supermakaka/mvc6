using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class ProductPriceHistory
    {
        public int Id { get; set; }

        [Required]
        public decimal OldPrice { get; set; }

        [Required]
        public decimal OldDiscount { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public ICollection<Product_ProductPriceHistory> Products { get; set; }
    }
}
