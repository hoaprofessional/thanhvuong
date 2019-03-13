using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class FixPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "QoutationDetail",
                nullable: false,
                computedColumnSql: "1",
                oldClrType: typeof(double),
                oldComputedColumnSql: "([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]");

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "OrderDetail",
                nullable: false,
                computedColumnSql: "1",
                oldClrType: typeof(double),
                oldComputedColumnSql: "([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]");


            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "QoutationDetail",
                newName: "UnitPriceSell");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "OrderDetail",
                newName: "UnitPriceSell");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPriceBuy",
                table: "QoutationDetail",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPriceBuy",
                table: "OrderDetail",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "QoutationDetail",
                nullable: false,
                computedColumnSql: "([VAT]*[UnitPriceSell] + [UnitPriceSell])*[ProductQuantity]",
                oldClrType: typeof(double),
                oldComputedColumnSql: "([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]");

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "OrderDetail",
                nullable: false,
                computedColumnSql: "([VAT]*[UnitPriceSell] + [UnitPriceSell])*[ProductQuantity]",
                oldClrType: typeof(double),
                oldComputedColumnSql: "([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "QoutationDetail",
                nullable: false,
                computedColumnSql: "1",
                oldClrType: typeof(double),
                oldComputedColumnSql: "([VAT]*[UnitPriceSell] + [UnitPriceSell])*[ProductQuantity]");

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "OrderDetail",
                nullable: false,
                computedColumnSql: "1",
                oldClrType: typeof(double),
                oldComputedColumnSql: "([VAT]*[UnitPriceSell] + [UnitPriceSell])*[ProductQuantity]");


            migrationBuilder.DropColumn(
                name: "UnitPriceBuy",
                table: "QoutationDetail");

            migrationBuilder.DropColumn(
                name: "UnitPriceBuy",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "UnitPriceSell",
                table: "QoutationDetail",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "UnitPriceSell",
                table: "OrderDetail",
                newName: "UnitPrice");

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "QoutationDetail",
                nullable: false,
                computedColumnSql: "([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]",
                oldClrType: typeof(double),
                oldComputedColumnSql: "([VAT]*[UnitPriceSell] + [UnitPriceSell])*[ProductQuantity]");

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "OrderDetail",
                nullable: false,
                computedColumnSql: "([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]",
                oldClrType: typeof(double),
                oldComputedColumnSql: "([VAT]*[UnitPriceSell] + [UnitPriceSell])*[ProductQuantity]");
        }
    }
}
