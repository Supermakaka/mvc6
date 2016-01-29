using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.ViewModels.Product
{
    using AutoMapper;
    using BusinessLogic.Models;
    using Mapping;

    public class Mappings : IViewModelMapping
    {
        public void Create(IConfiguration configuration)
        {
            configuration.CreateMap<Product, AdminProductListDatatableViewModel>()
                .ForMember(d => d.CreateDate, o => o.ResolveUsing<DateToFormattedStringResolver>().FromMember(s => s.CreateDate))
                .ForMember(d => d.ProductCategoryName, o => o.MapFrom(s => s.ProductSubCategory.ProductCategory.Title))
                .ForMember(d => d.ProductSubCategoryName, o => o.MapFrom(s => s.ProductSubCategory.Title))
                .ForMember(d => d.Count, o => o.MapFrom(s => s.ItemsCount))
                .IgnoreAllNonExisting();
        }
    }
}
