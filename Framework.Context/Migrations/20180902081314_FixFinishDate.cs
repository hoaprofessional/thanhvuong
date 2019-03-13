using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class FixFinishDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "DateBegin",
            //    schema: "Task",
            //    table: "Task",
            //    newName: "DateReception");

            //migrationBuilder.AddColumn<string>(
            //    name: "AssignerId",
            //    schema: "Task",
            //    table: "Task",
            //    maxLength: 450,
            //    nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishDate",
                schema: "Task",
                table: "Task",
                nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Result",
            //    schema: "Task",
            //    table: "Task",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Task_AssignerId",
            //    schema: "Task",
            //    table: "Task",
            //    column: "AssignerId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Task_AspNetUsers_AssignerId",
            //    schema: "Task",
            //    table: "Task",
            //    column: "AssignerId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Task_AspNetUsers_AssignerId",
            //    schema: "Task",
            //    table: "Task");

            //migrationBuilder.DropIndex(
            //    name: "IX_Task_AssignerId",
            //    schema: "Task",
            //    table: "Task");

            //migrationBuilder.DropColumn(
            //    name: "AssignerId",
            //    schema: "Task",
            //    table: "Task");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishDate",
                schema: "Task",
                table: "Task",
                nullable: false);

            //migrationBuilder.DropColumn(
            //    name: "Result",
            //    schema: "Task",
            //    table: "Task");

            //migrationBuilder.RenameColumn(
            //    name: "DateReception",
            //    schema: "Task",
            //    table: "Task",
            //    newName: "DateBegin");
        }
    }
}
