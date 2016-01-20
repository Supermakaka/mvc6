using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Data.Entity;

namespace BusinessLogic.Services
{
    using Models;

    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public void DisableOrEnable(User user)
        {
            user.Disabled = !user.Disabled;

            dataContext.SaveChanges();
        }

        public IQueryable<User> GetAllWithCompanies()
        {
            return dataContext.Users;
        }   
    }

    public interface IUserService : IService<User>
    {
        void DisableOrEnable(User user);
    }
}
