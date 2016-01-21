using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.ViewModels.Product
{
    public class AdminProductListDatatableViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Price { get; set; }

        public bool Visable { get; set; }

        public int Count { get; set; }

        public string CreateDate { get; set; }

        public string ProductCategoryName { get; set; }

        public string ProductSubCategoryName { get; set; }
    }
}
