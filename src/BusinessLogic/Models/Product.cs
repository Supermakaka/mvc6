using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string Price { get; set; }

        public decimal Weight { get; set; }

        public decimal Height { get; set; }

        public decimal Width { get; set; }

        public decimal Lenght { get; set; }

        public DateTime CreateDate { get; set; }

        public int ThumbId { get; set; }
        public FileInfo Thumb { get; set; } 

        public int ProductQtyInStore { get; set; }

        public int IsProductAvilable { get; set; }

        public virtual ICollection<Product_FileInfo> Pictures { get; set; }
        public ICollection<Product_ProductPriceHistory> Products { get; set; }
    }

    public class Product_FileInfo
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int FileId { get; set; }
        public virtual FileInfo File { get; set; }
    }

    public class Product_ProductPriceHistory
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ProductPriceHistorytId { get; set; }
        public ProductPriceHistory ProductPriceHistory { get; set; }
    }
}
