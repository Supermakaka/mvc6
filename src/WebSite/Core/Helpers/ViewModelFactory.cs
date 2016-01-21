using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.ViewModels.Product;

namespace WebSite.Core.Helpers
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IProductService productService;

        public ViewModelFactory(IProductService productService)
        {
            this.productService = productService;
        }

        public AdminProductListViewModel InitAdminProductListViewModel()
        {
            IQueryable<ProductCategory> productCategories = productService.GetAllProductCategories();

            if (productCategories.Any())
                return new AdminProductListViewModel(productCategories, productService.GetSubCategoriesByCategoryId(productCategories.FirstOrDefault().Id));
            
            return new AdminProductListViewModel(new List<ProductCategory>().AsQueryable(), new List<ProductSubCategory>().AsQueryable());
        }
    }

    public interface IViewModelFactory
    {
        AdminProductListViewModel InitAdminProductListViewModel();
    }
}
