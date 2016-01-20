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
            IQueryable<ProductCategory> ProductCategories = productService.GetAllProductCategories();

            return new AdminProductListViewModel(productService.GetAllProductCategories(), productService.GetSubCategoriesByCategoryId(ProductCategories.FirstOrDefault().Id));
        }
    }

    public interface IViewModelFactory
    {
        AdminProductListViewModel InitAdminProductListViewModel();
    }
}
