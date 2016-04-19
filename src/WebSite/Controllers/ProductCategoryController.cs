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
using WebSite.ViewModels.ProductsCategory.TableViewModels;
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

        #region Tables

        public IActionResult AdminProductCategoryListAjax(IDataTablesRequest request, ProductComponentsEnum type)
        {
            DataTablesResponse response;

            if (type == ProductComponentsEnum.Category)
            {
                IQueryable<ProductCategory> productCategores = productCategoryService.GetAllProductCategories();

                var res = request.ApplyToQuery(productCategores, opt => opt
                    .ForColumn("Title").EnableGlobalSearch()
                    .ForColumn("CreateDate").EnableGlobalSearch()
                    .ForColumn("Deleted").EnableGlobalSearch()
                );

                var model = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryDatatableViewModel>>(res.QueryFiltered);

                response = DataTablesResponse.Create(request, res.TotalRecords, res.TotalRecordsFiltered, model);
            }
            else if (type == ProductComponentsEnum.SubCategory)
            {
                IQueryable<ProductSubCategory> productSubCategories = productCategoryService.GetAllProductSubCategories();

                var res = request.ApplyToQuery(productSubCategories, opt => opt
                    .ForColumn("Title").EnableGlobalSearch()
                    .ForColumn("ProductCategory").EnableGlobalSearch().MapToProperty(s => s.ProductCategory.Title)
                    .ForColumn("CreateDate").EnableGlobalSearch()
                    .ForColumn("Deleted").EnableGlobalSearch()
                );

                var model = Mapper.Map<IEnumerable<ProductSubCategory>, IEnumerable<ProductSubCategoryDatatableViewModel>>(res.QueryFiltered);

                response = DataTablesResponse.Create(request, res.TotalRecords, res.TotalRecordsFiltered, model);
            }
            else if (type == ProductComponentsEnum.Property)
            {
                IQueryable<ProductProperty> productProperties = productPropertyService.GetAllNotDeleted();

                var res = request.ApplyToQuery(productProperties, opt => opt
                    .ForColumn("Title").EnableGlobalSearch()
                    .ForColumn("Value").EnableGlobalSearch()
                    .ForColumn("Unit").EnableGlobalSearch().MapToProperty(s => s.ProductCategory.Title)
                    .ForColumn("ProductCategory").EnableGlobalSearch().MapToProperty(s => s.ProductCategory.Title)
                    .ForColumn("ProductSubCategory").EnableGlobalSearch().MapToProperty(s => s.ProductCategory.Title)
                    .ForColumn("CreateDate").EnableGlobalSearch()
                    .ForColumn("Deleted").EnableGlobalSearch()
                );

                var model = Mapper.Map<IEnumerable<ProductProperty>, IEnumerable<ProductPropertyDatatableViewModel>>(res.QueryFiltered);

                response = DataTablesResponse.Create(request, res.TotalRecords, res.TotalRecordsFiltered, model);
            }
            else if (type == ProductComponentsEnum.Unit)
            {
                IQueryable<Unit> units = productPropertyService.GetAllNotDeletedUnits();

                var res = request.ApplyToQuery(units, opt => opt
                    .ForColumn("Title").EnableGlobalSearch()
                    .ForColumn("CreateDate").EnableGlobalSearch()
                    .ForColumn("Deleted").EnableGlobalSearch()
                );

                var model = Mapper.Map<IEnumerable<Unit>, IEnumerable<UnitDatatableViewModel>>(res.QueryFiltered);

                response = DataTablesResponse.Create(request, res.TotalRecords, res.TotalRecordsFiltered, model);
            }
            else if (type == ProductComponentsEnum.UnitType)
            {
                IQueryable<UnitType> unitTypes = productPropertyService.GetAllNotDeletedUnitTypes();

                var res = request.ApplyToQuery(unitTypes, opt => opt
                    .ForColumn("Title").EnableGlobalSearch()
                    .ForColumn("CreateDate").EnableGlobalSearch()
                    .ForColumn("Deleted").EnableGlobalSearch()
                );

                var model = Mapper.Map<IEnumerable<UnitType>, IEnumerable<UnitTypeDatatableViewModel>>(res.QueryFiltered);

                response = DataTablesResponse.Create(request, res.TotalRecords, res.TotalRecordsFiltered, model);
            }
            else
                return new DataTablesJsonResult(DataTablesResponse.Create(request, ""));

            return new DataTablesJsonResult(response, true);
        }

        #endregion

        public IActionResult List()
        {
            return View();
        }
    }

    #endregion

    #region ViewComponents

    #endregion
}


