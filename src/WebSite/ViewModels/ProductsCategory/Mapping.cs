using AutoMapper;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.ViewModels.Mapping;
using WebSite.ViewModels.ProductsCategory.TableViewModels;

namespace WebSite.ViewModels.ProductsCategory
{
    public class Mappings : IViewModelMapping
    {
        public void Create(IConfiguration configuration)
        {
            #region From Domain Model To View Model

            configuration.CreateMap<ProductProperty, ProductPropertyDatatableViewModel>()
                .ForMember(d => d.CreateDate, o => o.ResolveUsing<DateToFormattedStringResolver>().FromMember(s => s.CreateDate))
                .ForMember(d => d.Deleted, o => o.MapFrom(s => s.Deleted))
                .ForMember(d => d.ProductCategory, o => o.MapFrom(s => s.ProductCategory.Title))
                .ForMember(d => d.ProductSubCategory, o => o.MapFrom(s => s.ProductSubCategory.Title))
                .ForMember(d => d.Unit, o => o.MapFrom(s => s.Unit.Title))
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Value))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .IgnoreAllNonExisting();

            configuration.CreateMap<ProductCategory, ProductCategoryDatatableViewModel>()
                .ForMember(d => d.CreateDate, o => o.ResolveUsing<DateToFormattedStringResolver>().FromMember(s => s.CreateDate))
                .ForMember(d => d.Deleted, o => o.MapFrom(s => s.Deleted))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .IgnoreAllNonExisting();

            configuration.CreateMap<ProductSubCategory, ProductSubCategoryDatatableViewModel>()
                .ForMember(d => d.CreateDate, o => o.ResolveUsing<DateToFormattedStringResolver>().FromMember(s => s.CreateDate))
                .ForMember(d => d.Deleted, o => o.MapFrom(s => s.Deleted))
                .ForMember(d => d.ProductCategory, o => o.MapFrom(s => s.ProductCategory.Title))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .IgnoreAllNonExisting();

            configuration.CreateMap<Unit, UnitDatatableViewModel>()
                .ForMember(d => d.CreateDate, o => o.ResolveUsing<DateToFormattedStringResolver>().FromMember(s => s.CreateDate))
                .ForMember(d => d.Deleted, o => o.MapFrom(s => s.Deleted))
                .ForMember(d => d.UnitType, o => o.MapFrom(s => s.UnitType.Title))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .IgnoreAllNonExisting();

            configuration.CreateMap<UnitType, UnitTypeDatatableViewModel>()
                .ForMember(d => d.CreateDate, o => o.ResolveUsing<DateToFormattedStringResolver>().FromMember(s => s.CreateDate))
                .ForMember(d => d.Deleted, o => o.MapFrom(s => s.Deleted))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .IgnoreAllNonExisting();

            #endregion

            #region From View Model To Domain Model
            
            #endregion
        }
    }
}
