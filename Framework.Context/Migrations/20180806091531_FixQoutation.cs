using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class FixQoutation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "QoutationDetail",
                nullable: false,
                computedColumnSql: "[UnitPrice]",
                oldClrType: typeof(decimal),
                oldComputedColumnSql: "([VAT] + [DeliveryPrice] + [InstallationPrice] + [UnitPrice])*[ProductQuantity]");

            migrationBuilder.AlterColumn<double>(
                name: "VAT",
                table: "QoutationDetail",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.DropColumn(
                name: "DeliveryPrice",
                table: "QoutationDetail");

            migrationBuilder.DropColumn(
                name: "InstallationPrice",
                table: "QoutationDetail");

            migrationBuilder.AlterColumn<decimal>(
    name: "TotalPrice",
    table: "QoutationDetail",
    nullable: false,
    computedColumnSql: "([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]",
    oldClrType: typeof(decimal),
    oldComputedColumnSql: "[UnitPrice]");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "QoutationDetail",
                nullable: false,
                computedColumnSql: "[UnitPrice]",
                oldClrType: typeof(decimal),
                oldComputedColumnSql: "([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]");

            migrationBuilder.AddColumn<decimal>(
    name: "InstallationPrice",
    table: "QoutationDetail",
    nullable: false,
    defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "VAT",
                table: "QoutationDetail",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryPrice",
                table: "QoutationDetail",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "QoutationDetail",
                nullable: false,
                computedColumnSql: "([VAT] + [DeliveryPrice] + [InstallationPrice] + [UnitPrice])*[ProductQuantity]",
                oldClrType: typeof(decimal),
                oldComputedColumnSql: "[UnitPrice]");


        }
    }
}
