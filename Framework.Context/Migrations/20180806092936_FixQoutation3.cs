using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class FixQoutation3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "QoutationDetail",
                nullable: false,
                computedColumnSql: "([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]",
                oldClrType: typeof(decimal),
                oldComputedColumnSql: "([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "QoutationDetail",
                nullable: false,
                computedColumnSql: "([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]",
                oldClrType: typeof(double),
                oldComputedColumnSql: "([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]");
        }
    }
}
