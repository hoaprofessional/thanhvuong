using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddNotificationConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationPermissionConfiguration",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    NotificationCode = table.Column<string>(maxLength: 100, nullable: true),
                    NotificationDecription = table.Column<string>(maxLength: 1000, nullable: true),
                    PermissionValue = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationPermissionConfiguration", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationPermissionConfiguration");
        }
    }
}
