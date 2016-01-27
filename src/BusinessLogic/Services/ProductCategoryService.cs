using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductCategoryService : BaseService<ProductCategory>, IProductCategoryService
    {
        public ProductCategoryService(IDataContext dataContext) : base(dataContext)
        {
        }

        public IQueryable<ProductCategory> GetAllProductCategories()
        {
            return dataContext.ProductCategories.Where(s => !s.Deleted);
        }

        public IQueryable<ProductSubCategory> GetAllProductSubCategories()
        {
            return dataContext.ProductSubCategories.Where(s => !s.Deleted);
        }

        public IQueryable<ProductSubCategory> GetSubCategoriesByCategoryId(int? productSubcategoryId)
        {
            if (productSubcategoryId.HasValue)
                return dataContext.ProductSubCategories.Where(s => s.ProductCategoryId == productSubcategoryId && !s.Deleted);

            return new List<ProductSubCategory>().AsQueryable<ProductSubCategory>();
        }
    }

    public interface IProductCategoryService : IService<ProductCategory>
    {
        IQueryable<ProductCategory> GetAllProductCategories();
        IQueryable<ProductSubCategory> GetAllProductSubCategories();
        IQueryable<ProductSubCategory> GetSubCategoriesByCategoryId(int? productSubcategoryId);
    }
}
