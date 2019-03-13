using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class FixPriceOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Qoutation",
                newName: "TotalPriceSell");

            migrationBuilder.AddColumn<double>(
                name: "TotalPriceBuy",
                table: "Qoutation",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPriceBuy",
                table: "Qoutation");

            migrationBuilder.RenameColumn(
                name: "TotalPriceSell",
                table: "Qoutation",
                newName: "TotalPrice");
        }
    }
}
