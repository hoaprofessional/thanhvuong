using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddQoutationSide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuotesProcessedInterested",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Permission = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true),
                    QoutationStatusId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotesProcessedInterested", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotesStatusWaitingApprovalInterested",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Permission = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true),
                    QoutationStatusId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotesStatusWaitingApprovalInterested", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotesStatusWaitingProcessInterested",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Permission = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true),
                    QoutationStatusId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotesStatusWaitingProcessInterested", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuotesProcessedInterested");

            migrationBuilder.DropTable(
                name: "QuotesStatusWaitingApprovalInterested");

            migrationBuilder.DropTable(
                name: "QuotesStatusWaitingProcessInterested");
        }
    }
}
