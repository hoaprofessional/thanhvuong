using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddOrderInterested : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderProcessedInterested",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    IsSelf = table.Column<bool>(nullable: false),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    OrderStatusId = table.Column<string>(nullable: true),
                    OrderStatusStaffCreated = table.Column<string>(nullable: true),
                    Permission = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProcessedInterested", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatusWaitingApprovalInterested",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    IsSelf = table.Column<bool>(nullable: false),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Permission = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true),
                    QoutationStatusId = table.Column<string>(nullable: true),
                    QoutationStatusStaffCreated = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusWaitingApprovalInterested", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatusWaitingProcessInterested",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    IsSelf = table.Column<bool>(nullable: false),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Permission = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true),
                    QoutationStatusId = table.Column<string>(nullable: true),
                    QoutationStatusStaffCreated = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusWaitingProcessInterested", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProcessedInterested");

            migrationBuilder.DropTable(
                name: "OrderStatusWaitingApprovalInterested");

            migrationBuilder.DropTable(
                name: "OrderStatusWaitingProcessInterested");
        }
    }
}
