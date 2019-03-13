using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "OrderDetail",
                nullable: false,
                computedColumnSql: "([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]",
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "OrderDetail",
                nullable: false,
                oldClrType: typeof(double),
                oldComputedColumnSql: "([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]");
        }
    }
}
