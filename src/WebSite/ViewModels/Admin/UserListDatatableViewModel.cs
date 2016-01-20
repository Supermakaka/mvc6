using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebSite.ViewModels.Admin
{
    public class UserListDatatableViewModel
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual string Email { get; set; }
        public bool Visible { get; set; }
        public int Count { get; set; }
        public string Role { get; set; }
        public bool Disabled { get; set; }
        [Display(Name = "Created")]
        public string CreateDate { get; set; }
    }
}