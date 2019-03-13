using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class FixOrderInterested : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QoutationStatusStaffCreated",
                table: "OrderStatusWaitingProcessInterested",
                newName: "OrderStatusStaffCreated");

            migrationBuilder.RenameColumn(
                name: "QoutationStatusId",
                table: "OrderStatusWaitingProcessInterested",
                newName: "OrderStatusId");

            migrationBuilder.RenameColumn(
                name: "QoutationStatusStaffCreated",
                table: "OrderStatusWaitingApprovalInterested",
                newName: "OrderStatusStaffCreated");

            migrationBuilder.RenameColumn(
                name: "QoutationStatusId",
                table: "OrderStatusWaitingApprovalInterested",
                newName: "OrderStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderStatusStaffCreated",
                table: "OrderStatusWaitingProcessInterested",
                newName: "QoutationStatusStaffCreated");

            migrationBuilder.RenameColumn(
                name: "OrderStatusId",
                table: "OrderStatusWaitingProcessInterested",
                newName: "QoutationStatusId");

            migrationBuilder.RenameColumn(
                name: "OrderStatusStaffCreated",
                table: "OrderStatusWaitingApprovalInterested",
                newName: "QoutationStatusStaffCreated");

            migrationBuilder.RenameColumn(
                name: "OrderStatusId",
                table: "OrderStatusWaitingApprovalInterested",
                newName: "QoutationStatusId");
        }
    }
}
