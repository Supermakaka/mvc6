﻿using BusinessLogic.Models;
using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IDataContext dataContext) : base(dataContext) { }

        #region Private



        #endregion

        #region Public 

        public IQueryable<ProductCategory> GetAllProductCategories()
        {
            return dataContext.ProductCategories.Where(s => !s.Deleted);
        }

        public IQueryable<ProductSubCategory> GetAllProductSubCategories()
        {
            return dataContext.ProductSubCategories.Where(s => !s.Deleted);
        }

        public IQueryable<ProductSubCategory> GetSubCategoriesByCategoryId(int productSubcategoryId)
        {
            return dataContext.ProductSubCategories.Where(s => s.ProductCategoryId == productSubcategoryId && !s.Deleted);
        }

        #endregion
    }
}

public interface IProductService : IService<Product>
{
    IQueryable<ProductCategory> GetAllProductCategories();
    IQueryable<ProductSubCategory> GetAllProductSubCategories();
    IQueryable<ProductSubCategory> GetSubCategoriesByCategoryId(int productSubcategoryId);
}
