using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebSite.ViewModels.Admin
{
    public class CompanyFormViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}