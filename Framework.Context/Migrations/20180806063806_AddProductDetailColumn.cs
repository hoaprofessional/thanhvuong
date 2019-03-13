using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddProductDetailColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductDetail",
                table: "QoutationDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductNote",
                table: "QoutationDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductSize",
                table: "QoutationDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductUnit",
                table: "QoutationDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDetail",
                table: "QoutationDetail");

            migrationBuilder.DropColumn(
                name: "ProductNote",
                table: "QoutationDetail");

            migrationBuilder.DropColumn(
                name: "ProductSize",
                table: "QoutationDetail");

            migrationBuilder.DropColumn(
                name: "ProductUnit",
                table: "QoutationDetail");
        }
    }
}
