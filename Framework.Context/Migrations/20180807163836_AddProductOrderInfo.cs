using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddProductOrderInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductQuantity",
                table: "OrderDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "OrderDetail",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "OrderDetail",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "VAT",
                table: "OrderDetail",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductQuantity",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "VAT",
                table: "OrderDetail");
        }
    }
}
