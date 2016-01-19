using BusinessLogic.Models;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.ViewModels.Mapping;

namespace WebSite.ViewModels.Product
{
    public class AdminProductListViewModel
    {
        public IEnumerable<SelectListItem> ProductCategories { get; set; }
        public IEnumerable<SelectListItem> ProductSubCategories { get; set; }

        public AdminProductListViewModel(IQueryable<ProductCategory> productCategories, IQueryable<ProductSubCategory> productSubCategories)
        {
            ProductCategories = productCategories.ToSelectListItems(r => r.Name, r => r.Id, true);
            ProductSubCategories = productCategories.ToSelectListItems(r => r.Name, r => r.Id, true);
        }
    }
}
