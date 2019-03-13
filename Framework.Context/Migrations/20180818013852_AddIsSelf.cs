using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddIsSelf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelf",
                table: "QuotesStatusWaitingProcessInterested",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSelf",
                table: "QuotesStatusWaitingApprovalInterested",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSelf",
                table: "QuotesProcessedInterested",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelf",
                table: "QuotesStatusWaitingProcessInterested");

            migrationBuilder.DropColumn(
                name: "IsSelf",
                table: "QuotesStatusWaitingApprovalInterested");

            migrationBuilder.DropColumn(
                name: "IsSelf",
                table: "QuotesProcessedInterested");
        }
    }
}
