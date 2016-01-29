using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductPropertyService : BaseService<ProductProperty>, IProductPropertyService
    {
        public ProductPropertyService(DataContext dataContext) : base(dataContext)
        {

        }

        public IQueryable<ProductProperty> GetAllNotDeleted()
        {
            return base.GetMany(s => !s.Deleted);
        }

        public IQueryable<Unit> GetAllNotDeletedUnits()
        {
            return dataContext.Units.Where(s => !s.Deleted);
        }

        public IQueryable<UnitType> GetAllNotDeletedUnitTypes()
        {
            return dataContext.UnitTypes.Where(s => !s.Deleted);
        }
    }

    public interface IProductPropertyService : IService<ProductProperty>
    {
        IQueryable<ProductProperty> GetAllNotDeleted();
        IQueryable<Unit> GetAllNotDeletedUnits();
        IQueryable<UnitType> GetAllNotDeletedUnitTypes();
    }
}
