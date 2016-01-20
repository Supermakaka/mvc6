using BusinessLogic.Models;
using BusinessLogic.Services;
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

        public IActionResult AdminProductListAjax()
        {
            IQueryable<Product> Products = productService.GetAll();

            return View();
        }
    }
}
