using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.AspNet.Identity.EntityFramework;

namespace BusinessLogic.Models
{
    public class User: IdentityUser<int>
    {
        public bool Disabled { get; set; }

        public virtual int? CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<UserOrder> Orders { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public DateTime CreateDate { get; set; }

        public User() : base()
        {
        }

        public User(string userName) : base(userName)
        {
        }
    }

    public class UserRole : IdentityUserRole<int>
    {
        public Role Role { get; set; }
        public User User { get; set; }
    }

    public class UserLogin : IdentityUserLogin<int>
    {
    }

    public class UserClaim : IdentityUserClaim<int>
    {
    }

    public class RoleClaim : IdentityRoleClaim<int>
    {
    }

    public class Role : IdentityRole<int>
    {
        public Role() : base()
        {
        }

        public Role(string roleName) : base(roleName)
        {
        }
    }
}
