using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.ViewModels.ProductsCategory.TableViewModels
{
    public class BaseDatatableViewModel 
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Enabled { get; set; }

        public string CreateDate { get; set; }
    }
}
