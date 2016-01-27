using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductPropertyService : BaseService<ProductProperties>, IProductPropertyService
    {
        public ProductPropertyService(DataContext dataContext) : base(dataContext)
        {

        }
    }

    public interface IProductPropertyService : IService<ProductProperties>
    {

    }
}
