﻿using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using BusinessLogic.Models;

namespace WebSite.ViewModels.Admin
{
    using Mapping;

    public class Mappings : IViewModelMapping
    {
        public void Create(IConfiguration configuration)
        {
            configuration.CreateMap<Company, CompanyFormViewModel>();

            configuration.CreateMap<Company, CompanyListDatatableViewModel>();

            configuration.CreateMap<User, UserFormViewModel>()
                .ForMember(d => d.CompanyId, o => o.MapFrom(s => s.Company.Id))
                .IgnoreAllNonExisting();

            configuration.CreateMap<User, UserListDatatableViewModel>()
                .ForMember(d => d.CompanyName, o => o.MapFrom(s => s.Company.Name))
                .ForMember(d => d.CreateDate, o => o.ResolveUsing<DateToFormattedStringResolver>().FromMember(s => s.CreateDate)) 
                .ForMember(d => d.Role, o => o.ResolveUsing<UserRoleListToStringResolver>().FromMember(s => s));
        }
    }
}