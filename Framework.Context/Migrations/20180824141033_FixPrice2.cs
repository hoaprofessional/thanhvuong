using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class FixPrice2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "QoutationDetail");

            migrationBuilder.RenameColumn(
                name: "VAT",
                table: "QoutationDetail",
                newName: "VATSell");

            migrationBuilder.AddColumn<double>(
                name: "VATBuy",
                table: "QoutationDetail",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalPriceBuy",
                table: "QoutationDetail",
                nullable: false,
                computedColumnSql: "(VATBuy*UnitPriceBuy + UnitPriceBuy)*ProductQuantity");

            migrationBuilder.AddColumn<double>(
                name: "TotalPriceSell",
                table: "QoutationDetail",
                nullable: false,
                computedColumnSql: "(VATSell*UnitPriceSell + UnitPriceSell)*ProductQuantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "TotalPriceBuy",
                table: "QoutationDetail");

            migrationBuilder.DropColumn(
                name: "TotalPriceSell",
                table: "QoutationDetail");

            migrationBuilder.DropColumn(
                name: "VATBuy",
                table: "QoutationDetail");

            migrationBuilder.RenameColumn(
                name: "VATSell",
                table: "QoutationDetail",
                newName: "VAT");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "QoutationDetail",
                nullable: false,
                computedColumnSql: "([VAT]*[UnitPriceSell] + [UnitPriceSell])*[ProductQuantity]");
        }
    }
}
