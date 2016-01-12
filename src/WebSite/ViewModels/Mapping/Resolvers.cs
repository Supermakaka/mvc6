using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNet.Identity;

using AutoMapper;

using BusinessLogic.Models;

namespace WebSite.ViewModels.Mapping
{
    public class DateTimeToFormattedStringResolver : ValueResolver<DateTime?, string>
    {
        protected override string ResolveCore(DateTime? source)
        {
            if (!source.HasValue)
                return "";

            return source.Value.ToString("MM/dd/yyyy h:mm tt");
        }
    }

    public class DateToFormattedStringResolver : ValueResolver<DateTime?, string>
    {
        protected override string ResolveCore(DateTime? source)
        {
            if (!source.HasValue)
                return "";

            return source.Value.ToString("MM/dd/yyyy");
        }
    }

    public class UserRoleListToStringResolver : ValueResolver<User, string>
    {
        private UserManager<User> userManager;

        public UserRoleListToStringResolver(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        protected override string ResolveCore(User source)
        {
            var list = userManager.GetRolesAsync(source).Result;

            return String.Join(", ", list);
        }
    }
}