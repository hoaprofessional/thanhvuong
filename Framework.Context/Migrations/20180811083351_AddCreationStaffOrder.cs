using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddCreationStaffOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreationStaffId",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CreationStaffId",
                table: "Order",
                column: "CreationStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Staff_CreationStaffId",
                table: "Order",
                column: "CreationStaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Staff_CreationStaffId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CreationStaffId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CreationStaffId",
                table: "Order");
        }
    }
}
