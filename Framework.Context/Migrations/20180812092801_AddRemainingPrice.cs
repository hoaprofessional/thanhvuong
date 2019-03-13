using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddRemainingPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PaidPrice",
                table: "Order",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RemainingPrice",
                table: "Order",
                nullable: false,
                computedColumnSql: "[TotalPrice] - [PaidPrice]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidPrice",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "RemainingPrice",
                table: "Order");
        }
    }
}
