using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Data.Entity;

namespace BusinessLogic.Services
{
    using Models;

    public class UserOrderService : BaseService<Order>, IUserOrderService
    {
        public UserOrderService(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public IEnumerable<Order> GetAllOrdersWithUsers()
        {
            return dataContext.UserOrders.OfType<Order>()
                .Include(u => u.User)
                .ToList();
        }
    }

    public interface IUserOrderService : IService<Order>
    {
        IEnumerable<Order> GetAllOrdersWithUsers();
    }
}
