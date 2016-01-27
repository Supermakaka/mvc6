using BusinessLogic.Models;
using BusinessLogic.Services;
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
        private readonly IProductCategoryService productCategoryService;

        public ViewModelFactory(IProductService productService, IProductCategoryService productCategoryService)
        {
            this.productService = productService;
            this.productCategoryService = productCategoryService;
        }

        public AdminProductListViewModel InitAdminProductListViewModel()
        {
            IQueryable<ProductCategory> productCategories = productCategoryService.GetAllProductCategories();

            if (productCategories.Any())
                return new AdminProductListViewModel(productCategories, productCategoryService.GetSubCategoriesByCategoryId(productCategories.FirstOrDefault().Id));
            
            return new AdminProductListViewModel(new List<ProductCategory>().AsQueryable(), new List<ProductSubCategory>().AsQueryable());
        }
    }

    public interface IViewModelFactory
    {
        AdminProductListViewModel InitAdminProductListViewModel();
    }
}
