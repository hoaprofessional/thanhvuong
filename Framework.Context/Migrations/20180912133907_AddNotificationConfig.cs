using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddNotificationConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "QoutationSendNotificationConfigs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "QoutationSendNotificationConfigs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationUserName",
                table: "QoutationSendNotificationConfigs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTest",
                table: "QoutationSendNotificationConfigs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedTime",
                table: "QoutationSendNotificationConfigs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUserName",
                table: "QoutationSendNotificationConfigs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "QoutationSendNotificationConfigs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Protected",
                table: "QoutationSendNotificationConfigs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "QoutationSendNotificationConfigs");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "QoutationSendNotificationConfigs");

            migrationBuilder.DropColumn(
                name: "CreationUserName",
                table: "QoutationSendNotificationConfigs");

            migrationBuilder.DropColumn(
                name: "IsTest",
                table: "QoutationSendNotificationConfigs");

            migrationBuilder.DropColumn(
                name: "ModifiedTime",
                table: "QoutationSendNotificationConfigs");

            migrationBuilder.DropColumn(
                name: "ModifiedUserName",
                table: "QoutationSendNotificationConfigs");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "QoutationSendNotificationConfigs");

            migrationBuilder.DropColumn(
                name: "Protected",
                table: "QoutationSendNotificationConfigs");
        }
    }
}
