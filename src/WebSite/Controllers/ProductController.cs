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

namespace WebSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly IViewModelFactory viewModelFactory;
        private readonly IProductService productService;

        public ProductController(IProductService productService, IViewModelFactory viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
            this.productService = productService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminProductList()
        {
            return View(viewModelFactory.InitAdminProductListViewModel());
        }

        public IActionResult AdminProductListAjax(IDataTablesRequest request)
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
}
