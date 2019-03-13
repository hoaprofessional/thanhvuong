using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    EstimatedInstallationTime = table.Column<decimal>(nullable: false),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true),
                    QoutationId = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Qoutation_QoutationId",
                        column: x => x.QoutationId,
                        principalTable: "Qoutation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 2000, nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(maxLength: 450, nullable: true),
                    ProductDetail = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(maxLength: 450, nullable: true),
                    ProductNote = table.Column<string>(nullable: true),
                    ProductSize = table.Column<string>(nullable: true),
                    ProductUnit = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderEvent",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(maxLength: 450, nullable: true),
                    OrderStatusId = table.Column<string>(maxLength: 450, nullable: true),
                    Protected = table.Column<bool>(nullable: true),
                    StaffId = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderEvent_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderEvent_OrderStatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderEvent_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_QoutationId",
                table: "Order",
                column: "QoutationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEvent_OrderId",
                table: "OrderEvent",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEvent_OrderStatusId",
                table: "OrderEvent",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEvent_StaffId",
                table: "OrderEvent",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "OrderEvent");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "OrderStatus");
        }
    }
}
