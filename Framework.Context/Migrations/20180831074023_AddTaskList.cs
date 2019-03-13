using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddTaskList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateBegin",
                schema: "Task",
                table: "Task",
                newName: "FinishDate");

            migrationBuilder.AddColumn<string>(
                name: "AssignerId",
                schema: "Task",
                table: "Task",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateReception",
                schema: "Task",
                table: "Task",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                schema: "Task",
                table: "Task",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Result",
                schema: "Task",
                table: "Task",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_AssignerId",
                schema: "Task",
                table: "Task",
                column: "AssignerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_AspNetUsers_AssignerId",
                schema: "Task",
                table: "Task",
                column: "AssignerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_AspNetUsers_AssignerId",
                schema: "Task",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_AssignerId",
                schema: "Task",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "AssignerId",
                schema: "Task",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "DateReception",
                schema: "Task",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "Deadline",
                schema: "Task",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "Result",
                schema: "Task",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "FinishDate",
                schema: "Task",
                table: "Task",
                newName: "DateBegin");
        }
    }
}
