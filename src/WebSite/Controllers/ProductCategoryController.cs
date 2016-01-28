using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services;
using DataTables.AspNet.AspNet5;
using DataTables.AspNet.AspNet5.Extensions.Linq;
using DataTables.AspNet.Core;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Core.Helpers;
using WebSite.ViewModels.Product;
using static BusinessLogic.Helpers.Constants;

namespace WebSite.Controllers
{

    #region Controller

    public class ProductCategoryController : Controller
    {
        private readonly IViewModelFactory viewModelFactory;
        private readonly IProductService productService;
        private readonly IProductPropertyService productPropertyService;
        private readonly IProductCategoryService productCategoryService;

        public ProductCategoryController(IProductService productService, IViewModelFactory viewModelFactory, IProductPropertyService productPropertyService, IProductCategoryService productCategoryService)
        {
            this.viewModelFactory = viewModelFactory;
            this.productService = productService;
            this.productPropertyService = productPropertyService;
            this.productCategoryService = productCategoryService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminProductList()
        {
            return View(viewModelFactory.InitAdminProductListViewModel());
        }

        public IActionResult AdminProductCategoryListAjax(IDataTablesRequest request)
        {
            IQueryable<Product> products = productService.GetAllNotDeleted();

            var res = request.ApplyToQuery(products, opt => opt
                .ForColumn("Name")
                    .EnableGlobalSearch()
                .ForColumn("Price")
                    .EnableGlobalSearch()
                .ForColumn("Count")
                    .EnableGlobalSearch()
                    .MapToProperty(s => s.ItemsCount)
                .ForColumn("ProductCategoryName")
                    .EnableGlobalSearch()
                    .MapToProperty(s => s.ProductSubCategory.ProductCategory.Name)
                .ForColumn("ProductSubCategoryName")
                    .EnableGlobalSearch()
                    .MapToProperty(s => s.ProductSubCategory.Name)
            );

            var model = Mapper.Map<IEnumerable<Product>, IEnumerable<AdminProductListDatatableViewModel>>(res.QueryFiltered);

            var response = DataTablesResponse.Create(request, res.TotalRecords, res.TotalRecordsFiltered, model);

            return new DataTablesJsonResult(response, true);
        }

        public IActionResult AdminProductSubCategoryListAjax(IDataTablesRequest request)
        {
            IQueryable<Product> products = productService.GetAllNotDeleted();

            var res = request.ApplyToQuery(products, opt => opt
                .ForColumn("Name")
                    .EnableGlobalSearch()
                .ForColumn("Price")
                    .EnableGlobalSearch()
                .ForColumn("Count")
                    .EnableGlobalSearch()
                    .MapToProperty(s => s.ItemsCount)
                .ForColumn("ProductCategoryName")
                    .EnableGlobalSearch()
                    .MapToProperty(s => s.ProductSubCategory.ProductCategory.Name)
                .ForColumn("ProductSubCategoryName")
                    .EnableGlobalSearch()
                    .MapToProperty(s => s.ProductSubCategory.Name)
            );

            var model = Mapper.Map<IEnumerable<Product>, IEnumerable<AdminProductListDatatableViewModel>>(res.QueryFiltered);

            var response = DataTablesResponse.Create(request, res.TotalRecords, res.TotalRecordsFiltered, model);

            return new DataTablesJsonResult(response, true);
        }

        public IActionResult AdminProductPropertiesListAjax(IDataTablesRequest request)
        {
            IQueryable<Product> products = productService.GetAllNotDeleted();

            var res = request.ApplyToQuery(products, opt => opt
                .ForColumn("Name")
                    .EnableGlobalSearch()
                .ForColumn("Price")
                    .EnableGlobalSearch()
                .ForColumn("Count")
                    .EnableGlobalSearch()
                    .MapToProperty(s => s.ItemsCount)
                .ForColumn("ProductCategoryName")
                    .EnableGlobalSearch()
                    .MapToProperty(s => s.ProductSubCategory.ProductCategory.Name)
                .ForColumn("ProductSubCategoryName")
                    .EnableGlobalSearch()
                    .MapToProperty(s => s.ProductSubCategory.Name)
            );

            var model = Mapper.Map<IEnumerable<Product>, IEnumerable<AdminProductListDatatableViewModel>>(res.QueryFiltered);

            var response = DataTablesResponse.Create(request, res.TotalRecords, res.TotalRecordsFiltered, model);

            return new DataTablesJsonResult(response, true);

        }
    }

    #endregion

    #region ViewComponents

    public class ProductCategoriesTableViewComponent : ViewComponent
    {
        private readonly IViewModelFactory viewModelFactory;
        private readonly IProductService productService;
        private readonly IProductPropertyService productPropertyService;
        private readonly IProductCategoryService productCategoryService;

        public ProductCategoriesTableViewComponent(IProductService productService, IViewModelFactory viewModelFactory, IProductPropertyService productPropertyService, IProductCategoryService productCategoryService)
        {
            this.viewModelFactory = viewModelFactory;
            this.productService = productService;
            this.productPropertyService = productPropertyService;
            this.productCategoryService = productCategoryService;
        }

        public IViewComponentResult Invoke(ProductComponentsEnum type)
        {
            if(type == ProductComponentsEnum.Category)
            {
                IQueryable<Product> products = productService.GetAllNotDeleted();

                var res = request.ApplyToQuery(products, opt => opt
                    .ForColumn("Name")
                        .EnableGlobalSearch()
                    .ForColumn("Price")
                        .EnableGlobalSearch()
                    .ForColumn("Count")
                        .EnableGlobalSearch()
                        .MapToProperty(s => s.ItemsCount)
                    .ForColumn("ProductCategoryName")
                        .EnableGlobalSearch()
                        .MapToProperty(s => s.ProductSubCategory.ProductCategory.Name)
                    .ForColumn("ProductSubCategoryName")
                        .EnableGlobalSearch()
                        .MapToProperty(s => s.ProductSubCategory.Name)
                );

                var model = Mapper.Map<IEnumerable<Product>, IEnumerable<AdminProductListDatatableViewModel>>(res.QueryFiltered);

                var response = DataTablesResponse.Create(request, res.TotalRecords, res.TotalRecordsFiltered, model);
            }                

            return View(model: "Hello World");
        }
    }

    #endregion
}
