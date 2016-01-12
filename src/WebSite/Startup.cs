using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using AutoMapper;
using DataTables.AspNet.AspNet5;

using BusinessLogic.Models;
using BusinessLogic.Services;

namespace WebSite
{
    using Services;
    using Core.Config;
    using ViewModels.Mapping;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Read settings from config
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<DevelopmentSettings>(Configuration.GetSection("DevelopmentSettings"));

            // Add Entity Framework services to the services container.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<DataContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            // Add Identity services with custom UserStore and RoleStore with EF6 support to the services container.
            services.AddIdentity<User, Role>(o => {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonLetterOrDigit = false;
                o.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<DataContext, int>()
            .AddDefaultTokenProviders();

            // Add MVC services to the services container.
            services.AddMvc();

            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            // services.AddWebApiConventions();

            // Add application services.
            AddApplicationServices(services);

            services.RegisterDataTables();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, Core.Helpers.DevelopmentDefaultData defaultData)
        {
            CreateMappings(app.ApplicationServices);

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                // Create roles and admin user
                defaultData.CreateIfNoDatabaseExists().Wait();

                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    //    .CreateScope())
                    //{
                    //    serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                    //         .Database.Migrate();
                    //}
                }
                catch { }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<Core.Helpers.DevelopmentDefaultData>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            //Business Logic
            services.AddScoped<IDataContext, DataContext>(serviceProvider => serviceProvider.GetService<DataContext>());
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserOrderService, UserOrderService>();

            //AutoMapper resolvers
            services.AddTransient<UserRoleListToStringResolver>();
            services.AddTransient<DateToFormattedStringResolver>();
        }

        private void CreateMappings(IServiceProvider serviceProvider)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.ConstructServicesUsing(type => serviceProvider.GetService(type));

                var mappings = typeof(IViewModelMapping).Assembly.GetExportedTypes()
                    .Where(x => !x.IsAbstract && typeof(IViewModelMapping).IsAssignableFrom(x))
                    .Select(Activator.CreateInstance)
                    .Cast<IViewModelMapping>();

                foreach (var m in mappings)
                    m.Create(Mapper.Configuration);
            });


            Mapper.AssertConfigurationIsValid();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
