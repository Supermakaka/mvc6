using BusinessLogic.Models;
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

        public IQueryable<Product> GetAllNotDeleted()
        {
            return base.GetMany(s => !s.Deleted);
        }

        #endregion
    }
}

public interface IProductService : IService<Product>
{
    IQueryable<Product> GetAllNotDeleted();
}
