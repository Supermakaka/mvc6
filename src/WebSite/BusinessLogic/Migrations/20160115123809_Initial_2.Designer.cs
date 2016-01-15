using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using BusinessLogic.Models;

namespace BusinessLogic.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20160115123809_Initial_2")]
    partial class Initial_2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusinessLogic.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("City");

                    b.Property<int>("StateId");

                    b.Property<int>("UserId");

                    b.Property<string>("Zip");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BusinessLogic.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Discount");

                    b.Property<string>("Name");

                    b.Property<int>("SubCategoryId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BusinessLogic.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BusinessLogic.Models.FileInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Extension")
                        .IsRequired();

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<int>("FileTypeId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BusinessLogic.Models.FileType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Name");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BusinessLogic.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<DateTime>("OrderDate");

                    b.Property<int>("Price");

                    b.Property<int?>("ShipAddressId");

                    b.Property<string>("TrackingNumber");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BusinessLogic.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<decimal>("Height");

                    b.Property<int>("IsProductAvilable");

                    b.Property<decimal>("Lenght");

                    b.Property<string>("LongDescription");

                    b.Property<string>("Name");

                    b.Property<string>("Price");

                    b.Property<int>("ProductQtyInStore");

                    b.Property<string>("ShortDescription");

                    b.Property<int>("ThumbId");

                    b.Property<decimal>("Weight");

                    b.Property<decimal>("Width");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BusinessLogic.Models.Product_FileInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FileInfoId");

                    b.Property<int?>("FileTypeId");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BusinessLogic.Models.Product_ProductPriceHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProductId");

                    b.Property<int?>("ProductPriceHistoryId");

                    b.Property<int>("ProductPriceHistorytId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BusinessLogic.Models.ProductPriceHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<decimal>("OldDiscount");

                    b.Property<decimal>("OldPrice");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BusinessLogic.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "Role");
                });

            modelBuilder.Entity("BusinessLogic.Models.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BusinessLogic.Models.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Discount");

                    b.Property<string>("Name");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("BusinessLogic.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("CompanyId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("Disabled");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<int?>("UserId");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "User");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "RoleClaim");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "UserClaim");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "UserRole");
                });

            modelBuilder.Entity("BusinessLogic.Models.Address", b =>
                {
                    b.HasOne("BusinessLogic.Models.State")
                        .WithMany()
                        .HasForeignKey("StateId");

                    b.HasOne("BusinessLogic.Models.User")
                        .WithOne()
                        .HasForeignKey("BusinessLogic.Models.Address", "UserId");
                });

            modelBuilder.Entity("BusinessLogic.Models.Category", b =>
                {
                    b.HasOne("BusinessLogic.Models.SubCategory")
                        .WithMany()
                        .HasForeignKey("SubCategoryId");
                });

            modelBuilder.Entity("BusinessLogic.Models.FileInfo", b =>
                {
                    b.HasOne("BusinessLogic.Models.FileType")
                        .WithMany()
                        .HasForeignKey("FileTypeId");
                });

            modelBuilder.Entity("BusinessLogic.Models.Order", b =>
                {
                    b.HasOne("BusinessLogic.Models.Address")
                        .WithMany()
                        .HasForeignKey("ShipAddressId");

                    b.HasOne("BusinessLogic.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BusinessLogic.Models.Product", b =>
                {
                    b.HasOne("BusinessLogic.Models.FileInfo")
                        .WithMany()
                        .HasForeignKey("ThumbId");
                });

            modelBuilder.Entity("BusinessLogic.Models.Product_FileInfo", b =>
                {
                    b.HasOne("BusinessLogic.Models.FileInfo")
                        .WithMany()
                        .HasForeignKey("FileInfoId");

                    b.HasOne("BusinessLogic.Models.FileType")
                        .WithMany()
                        .HasForeignKey("FileTypeId");

                    b.HasOne("BusinessLogic.Models.Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("BusinessLogic.Models.Product_ProductPriceHistory", b =>
                {
                    b.HasOne("BusinessLogic.Models.Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("BusinessLogic.Models.ProductPriceHistory")
                        .WithMany()
                        .HasForeignKey("ProductPriceHistoryId");
                });

            modelBuilder.Entity("BusinessLogic.Models.User", b =>
                {
                    b.HasOne("BusinessLogic.Models.Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("BusinessLogic.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("BusinessLogic.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("BusinessLogic.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<int>", b =>
                {
                    b.HasOne("BusinessLogic.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("BusinessLogic.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
