using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class addQoutationStatusStaffCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QoutationStatusStaffCreated",
                table: "QuotesStatusWaitingProcessInterested",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QoutationStatusStaffCreated",
                table: "QuotesStatusWaitingApprovalInterested",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QoutationStatusStaffCreated",
                table: "QuotesProcessedInterested",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QoutationStatusStaffCreated",
                table: "QuotesStatusWaitingProcessInterested");

            migrationBuilder.DropColumn(
                name: "QoutationStatusStaffCreated",
                table: "QuotesStatusWaitingApprovalInterested");

            migrationBuilder.DropColumn(
                name: "QoutationStatusStaffCreated",
                table: "QuotesProcessedInterested");
        }
    }
}
