using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AssignWorkUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignWorkUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    AssignToRole = table.Column<string>(maxLength: 100, nullable: true),
                    AssignerRole = table.Column<string>(maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignWorkUsers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignWorkUsers");
        }
    }
}
