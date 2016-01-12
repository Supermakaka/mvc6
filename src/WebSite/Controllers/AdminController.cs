using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;

using DataTables.AspNet.Core;
using DataTables.AspNet.AspNet5;
using DataTables.AspNet.AspNet5.Extensions.Linq;
using AutoMapper;

using BusinessLogic.Services;
using BusinessLogic.Models;

namespace WebSite.Controllers
{
    using ViewModels.Admin;

    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private UserManager<User> userManager;
        private RoleManager<Role> roleManager;
        private IUserService userService;
        private ICompanyService companyService;
        private IUserOrderService orderService;

        public AdminController(UserManager<User> userManager, RoleManager<Role> roleManager, IUserService userService, ICompanyService companyService, IUserOrderService orderService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userService = userService;
            this.companyService = companyService;
            this.orderService = orderService;
        }

        public IActionResult UserList()
        {
            var model = new UserListViewModel(roleManager.Roles.OrderBy(r => r.Name).ToList());

            return View(model);
        }

        public IActionResult UserListAjax(IDataTablesRequest request)
        {
            var users = userService.GetAllWithCompanies();

            var res = request.ApplyToQuery(users, opt => opt
                .ForColumn("Email")
                    .EnableGlobalSearch()
                .ForColumn("FirstName")
                    .EnableGlobalSearch()
                .ForColumn("LastName")
                    .EnableGlobalSearch()
                .ForColumn("CompanyName")
                    .MapToProperty(u => u.Company.Name)
                    .EnableGlobalSearch()
                    .IgnoreWhenSorting()
                .ForColumn("Role")
                    .SearchUsing((u, val) => u.Roles.Any(r => r.RoleId == int.Parse(val)))
                    .IgnoreWhenSorting()
            );

            var model = Mapper.Map<IEnumerable<User>, IEnumerable<UserListDatatableViewModel>>(res.QueryFiltered);

            var response = DataTablesResponse.Create(request, res.TotalRecords, res.TotalRecordsFiltered, model);

            return new DataTablesJsonResult(response, true);
        }

        public IActionResult CompanyList(int? page)
        {
            return View();
        }

        public IActionResult CompanyListAjax(IDataTablesRequest request)
        {
            var companies = companyService.GetAll();

            var res = request.ApplyToQuery(companies);

            var model = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyListDatatableViewModel>>(res.QueryFiltered);

            var response = DataTablesResponse.Create(request, res.TotalRecords, res.TotalRecordsFiltered, model);

            return new DataTablesJsonResult(response, true);
        }

        [AutoMap(typeof(User), typeof(UserListDatatableViewModel))]
        public IActionResult DisableOrEnableUser(int id)
        {
            var user = userService.GetById(id);

            userService.DisableOrEnable(user);

            return Json(new { success = true });
        }

        public IActionResult ConfirmDeleteUser(int id)
        {
            var user = userService.GetById(id);

            var model = Mapper.Map<User, UserFormViewModel>(user);

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            userService.Delete(userService.GetById(id));

            return Json(new { success = true });
        }

        public IActionResult CreateUser()
        {
            var companies = companyService.GetAll().ToList();

            var model = new UserFormViewModel(companies);

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult CreateUser(UserFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User();

                newUser.UserName = model.Email;
                newUser.CompanyId = model.CompanyId;
                newUser.FirstName = model.FirstName;
                newUser.LastName = model.LastName;
                newUser.Email = model.Email;
                newUser.Disabled = model.Disabled;
                newUser.CreateDate = DateTime.Now;

                var result = userManager.CreateAsync(newUser, model.Password).Result;

                if (result.Succeeded)
                {
                    result = userManager.AddToRoleAsync(newUser, RoleNames.User).Result;

                    if (result.Succeeded)
                        return Json(new { success = true });
                }

                CollectIdentityErrors(result);
            }

            var m = new UserFormViewModel(companyService.GetAll().ToList());

            return PartialView(m);
        }

        public IActionResult EditUser(int id)
        {
            var user = userService.GetByIdWithCompany(id);

            var model = Mapper.Map(user, new UserFormViewModel(companyService.GetAll().ToList()));

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult EditUser(UserFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userToEdit = userManager.FindByIdAsync(model.Id.ToString()).Result;

                userToEdit.UserName = model.Email;
                userToEdit.CompanyId = model.CompanyId;
                userToEdit.FirstName = model.FirstName;
                userToEdit.LastName = model.LastName;
                userToEdit.Email = model.Email;
                userToEdit.Disabled = model.Disabled;

                var result = userManager.UpdateAsync(userToEdit).Result;

                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(model.Password))
                    {
                        string resetToken = userManager.GeneratePasswordResetTokenAsync(userToEdit).Result;
                        result = userManager.ResetPasswordAsync(userToEdit, resetToken, model.Password).Result;

                        if (result.Succeeded)
                            return Json(new { success = true });
                    }
                    else
                        return Json(new { success = true });
                }

                CollectIdentityErrors(result);
            }

            var m = new UserFormViewModel(companyService.GetAll().ToList());

            return PartialView(m);
        }

        public IActionResult CreateCompany()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult CreateCompany(CompanyFormViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView(model);

            var newCompany = new Company();

            newCompany.Name = model.Name;

            companyService.Add(newCompany);

            return Json(new { success = true });
        }

        public IActionResult EditCompany(int id)
        {
            var company = companyService.GetById(id);

            var model = Mapper.Map<Company, CompanyFormViewModel>(company);

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult EditCompany(CompanyFormViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView(model);
            
            var companyToEdit = companyService.GetById(model.Id.Value);

            companyToEdit.Name = model.Name;

            companyService.Update(companyToEdit);

            return Json(new { success = true });
        }

        public IActionResult ConfirmDeleteCompany(int id)
        {
            var company = companyService.GetById(id);

            var model = Mapper.Map<Company, CompanyFormViewModel>(company);

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult DeleteCompany(int id)
        {
            if (userService.GetMany(u => u.CompanyId == id).Count() > 0)
                return Json(new { success = false, message = "Cannot delete company because company has users." });

            companyService.Delete(companyService.GetById(id));

            return Json(new { success = true });            
        }

        private void CollectIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}