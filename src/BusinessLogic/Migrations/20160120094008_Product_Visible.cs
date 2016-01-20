using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace BusinessLogic.Migrations
{
    public partial class Product_Visible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_FileInfo_FileType_FileTypeId", table: "FileInfo");
            migrationBuilder.DropForeignKey(name: "FK_Product_ProductSubCategory_ProductSubCategoryId", table: "Product");
            migrationBuilder.DropForeignKey(name: "FK_ProductSubCategory_ProductCategory_ProductCategoryId", table: "ProductSubCategory");
            migrationBuilder.DropForeignKey(name: "FK_UserOrder_User_UserId", table: "UserOrder");
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<int>_Role_RoleId", table: "RoleClaim");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<int>_User_UserId", table: "UserClaim");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<int>_User_UserId", table: "UserLogin");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<int>_Role_RoleId", table: "UserRole");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<int>_User_UserId", table: "UserRole");
            migrationBuilder.AddColumn<int>(
                name: "ItemsCount",
                table: "Product",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Product",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddForeignKey(
                name: "FK_FileInfo_FileType_FileTypeId",
                table: "FileInfo",
                column: "FileTypeId",
                principalTable: "FileType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductSubCategory_ProductSubCategoryId",
                table: "Product",
                column: "ProductSubCategoryId",
                principalTable: "ProductSubCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategory_ProductCategory_ProductCategoryId",
                table: "ProductSubCategory",
                column: "ProductCategoryId",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_UserOrder_User_UserId",
                table: "UserOrder",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<int>_Role_RoleId",
                table: "RoleClaim",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<int>_User_UserId",
                table: "UserClaim",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<int>_User_UserId",
                table: "UserLogin",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<int>_Role_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<int>_User_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_FileInfo_FileType_FileTypeId", table: "FileInfo");
            migrationBuilder.DropForeignKey(name: "FK_Product_ProductSubCategory_ProductSubCategoryId", table: "Product");
            migrationBuilder.DropForeignKey(name: "FK_ProductSubCategory_ProductCategory_ProductCategoryId", table: "ProductSubCategory");
            migrationBuilder.DropForeignKey(name: "FK_UserOrder_User_UserId", table: "UserOrder");
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<int>_Role_RoleId", table: "RoleClaim");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<int>_User_UserId", table: "UserClaim");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<int>_User_UserId", table: "UserLogin");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<int>_Role_RoleId", table: "UserRole");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<int>_User_UserId", table: "UserRole");
            migrationBuilder.DropColumn(name: "ItemsCount", table: "Product");
            migrationBuilder.DropColumn(name: "Visible", table: "Product");
            migrationBuilder.AddForeignKey(
                name: "FK_FileInfo_FileType_FileTypeId",
                table: "FileInfo",
                column: "FileTypeId",
                principalTable: "FileType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductSubCategory_ProductSubCategoryId",
                table: "Product",
                column: "ProductSubCategoryId",
                principalTable: "ProductSubCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategory_ProductCategory_ProductCategoryId",
                table: "ProductSubCategory",
                column: "ProductCategoryId",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UserOrder_User_UserId",
                table: "UserOrder",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<int>_Role_RoleId",
                table: "RoleClaim",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<int>_User_UserId",
                table: "UserClaim",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<int>_User_UserId",
                table: "UserLogin",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<int>_Role_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<int>_User_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
