using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.ViewModels.ProductsCategory.TableViewModels
{
    public class ProductPropertyDatatableViewModel : BaseDatatableViewModel
    {
        public string Unit { get; set; }

        public string Value { get; set; }

        public string ProductCategory { get; set; }

        public string ProductSubCategory { get; set; }
    }
}
