using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class ChangeQoutationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Qoutation_QoutationId",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "QoutationId",
                table: "Order",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Qoutation_QoutationId",
                table: "Order",
                column: "QoutationId",
                principalTable: "Qoutation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Qoutation_QoutationId",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "QoutationId",
                table: "Order",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Qoutation_QoutationId",
                table: "Order",
                column: "QoutationId",
                principalTable: "Qoutation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
