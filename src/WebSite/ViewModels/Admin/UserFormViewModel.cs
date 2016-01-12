using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.Rendering;

using ExpressiveAnnotations.Attributes;

using BusinessLogic.Models;

namespace WebSite.ViewModels.Admin
{
    using Mapping;

    public class UserFormViewModel
    {
        public int? Id { get; set; }

        public IEnumerable<SelectListItem> Companies { get; set; }

        [Display(Name = "Company")]
        public int? CompanyId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [RequiredIf("Id == null", ErrorMessage = "The Password field is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public virtual string Email { get; set; }

        public bool Disabled { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public UserFormViewModel()
        { }
        
        public UserFormViewModel(IList<Company> companies)
        {
            Companies = companies.ToSelectListItems(r => r.Name, r => r.Id, true);
        }
    }
}