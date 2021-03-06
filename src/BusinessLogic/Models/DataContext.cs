﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.ChangeTracking;

namespace BusinessLogic.Models
{
    public class DataContext : IdentityDbContext<User, Role, int>, IDataContext, IDisposable
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<UserOrder> UserOrders { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Change Asp.Net Identity default table names
            builder.Entity<User>().ToTable("User");
            builder.Entity<Role>().ToTable("Role");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRole");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
        }
    }

    public interface IDataContext
    {
        DatabaseFacade Database { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet<Company> Companies { get; set; }

        DbSet<UserOrder> UserOrders { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<Role> Roles { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
