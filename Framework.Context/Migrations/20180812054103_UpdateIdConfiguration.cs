using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class UpdateIdConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TableName",
                table: "IdConfiguration",
                newName: "Prefix");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prefix",
                table: "IdConfiguration",
                newName: "TableName");
        }
    }
}
