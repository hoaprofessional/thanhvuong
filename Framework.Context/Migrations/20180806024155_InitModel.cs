using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class InitModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Task");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CacheDatas",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    Expired = table.Column<bool>(nullable: false),
                    ExpiredDate = table.Column<DateTime>(nullable: false),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CacheDatas", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    Address = table.Column<string>(maxLength: 5000, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 1000, nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 450, nullable: true),
                    Protected = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommonConfigurations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    HeadQuarter = table.Column<string>(nullable: true),
                    Hotline = table.Column<string>(nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loggers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    LogType = table.Column<string>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loggers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    IsTest = table.Column<bool>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true),
                    StaffId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    Decription = table.Column<string>(nullable: true),
                    Images = table.Column<string>(maxLength: 2000, nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Protected = table.Column<bool>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QoutationStatus",
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
                    table.PrimaryKey("PK_QoutationStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priority",
                schema: "Task",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PriorityValue = table.Column<int>(nullable: false),
                    Protected = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskStatus",
                schema: "Task",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    Decription = table.Column<string>(maxLength: 200, nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkStatus",
                schema: "Task",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    Decription = table.Column<string>(maxLength: 200, nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserObject",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    DefaultCarrer = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    RoleId = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserObject_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Work",
                schema: "Task",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    DateBegin = table.Column<DateTime>(nullable: false),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PriorityId = table.Column<string>(maxLength: 450, nullable: true),
                    Protected = table.Column<bool>(nullable: true),
                    TimeExpired = table.Column<DateTime>(nullable: false),
                    WorkStatusId = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Work", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Work_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalSchema: "Task",
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Work_WorkStatus_WorkStatusId",
                        column: x => x.WorkStatusId,
                        principalSchema: "Task",
                        principalTable: "WorkStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Carrer = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    IdentityCardNumber = table.Column<string>(nullable: true),
                    IsBanned = table.Column<bool>(nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Note = table.Column<string>(nullable: true),
                    ObjectId = table.Column<string>(maxLength: 450, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    Protected = table.Column<bool>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserObject_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "UserObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    Address = table.Column<string>(maxLength: 2000, nullable: true),
                    Career = table.Column<string>(maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    IdentityCard = table.Column<string>(maxLength: 450, nullable: true),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true),
                    UserId = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                schema: "Task",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    AssignToId = table.Column<string>(maxLength: 450, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    DateBegin = table.Column<DateTime>(nullable: false),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PriorityId = table.Column<string>(maxLength: 450, nullable: true),
                    Protected = table.Column<bool>(nullable: true),
                    TaskStatusId = table.Column<string>(maxLength: 450, nullable: true),
                    WorkId = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_AspNetUsers_AssignToId",
                        column: x => x.AssignToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalSchema: "Task",
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_TaskStatus_TaskStatusId",
                        column: x => x.TaskStatusId,
                        principalSchema: "Task",
                        principalTable: "TaskStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_Work_WorkId",
                        column: x => x.WorkId,
                        principalSchema: "Task",
                        principalTable: "Work",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Qoutation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: true),
                    ClientId = table.Column<string>(maxLength: 450, nullable: true),
                    ConfirmStaffId = table.Column<string>(maxLength: 450, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    DeliveryPlace = table.Column<string>(maxLength: 1000, nullable: true),
                    EstimatedInstallationTime = table.Column<decimal>(nullable: false),
                    IsTest = table.Column<bool>(nullable: true),
                    ManagerId = table.Column<string>(maxLength: 450, nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PaymentMethod = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: true),
                    QoutationStatusId = table.Column<string>(nullable: true),
                    QouteStaffId = table.Column<string>(maxLength: 450, nullable: true),
                    RejectReason = table.Column<string>(nullable: true),
                    SalesAdminId = table.Column<string>(nullable: true),
                    TotalPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qoutation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qoutation_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Qoutation_Staff_ConfirmStaffId",
                        column: x => x.ConfirmStaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Qoutation_Staff_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Qoutation_QoutationStatus_QoutationStatusId",
                        column: x => x.QoutationStatusId,
                        principalTable: "QoutationStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Qoutation_Staff_QouteStaffId",
                        column: x => x.QouteStaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Qoutation_Staff_SalesAdminId",
                        column: x => x.SalesAdminId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QoutationDetail",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Active = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreationUserName = table.Column<string>(nullable: true),
                    DeliveryPrice = table.Column<decimal>(nullable: false),
                    InstallationPrice = table.Column<decimal>(nullable: false),
                    IsTest = table.Column<bool>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedUserName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(maxLength: 450, nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductQuantity = table.Column<int>(nullable: false),
                    Protected = table.Column<bool>(nullable: true),
                    QoutationId = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false, computedColumnSql: "([VAT] + [DeliveryPrice] + [InstallationPrice] + [UnitPrice])*[ProductQuantity]"),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    VAT = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QoutationDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QoutationDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QoutationDetail_Qoutation_QoutationId",
                        column: x => x.QoutationId,
                        principalTable: "Qoutation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QoutationEvent",
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
                    Protected = table.Column<bool>(nullable: true),
                    QoutationId = table.Column<int>(nullable: false),
                    QoutationStatusId = table.Column<string>(maxLength: 450, nullable: true),
                    StaffId = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QoutationEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QoutationEvent_Qoutation_QoutationId",
                        column: x => x.QoutationId,
                        principalTable: "Qoutation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QoutationEvent_QoutationStatus_QoutationStatusId",
                        column: x => x.QoutationStatusId,
                        principalTable: "QoutationStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QoutationEvent_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ObjectId",
                table: "AspNetUsers",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Qoutation_ClientId",
                table: "Qoutation",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Qoutation_ConfirmStaffId",
                table: "Qoutation",
                column: "ConfirmStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Qoutation_ManagerId",
                table: "Qoutation",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Qoutation_QoutationStatusId",
                table: "Qoutation",
                column: "QoutationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Qoutation_QouteStaffId",
                table: "Qoutation",
                column: "QouteStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Qoutation_SalesAdminId",
                table: "Qoutation",
                column: "SalesAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_QoutationDetail_ProductId",
                table: "QoutationDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QoutationDetail_QoutationId",
                table: "QoutationDetail",
                column: "QoutationId");

            migrationBuilder.CreateIndex(
                name: "IX_QoutationEvent_QoutationId",
                table: "QoutationEvent",
                column: "QoutationId");

            migrationBuilder.CreateIndex(
                name: "IX_QoutationEvent_QoutationStatusId",
                table: "QoutationEvent",
                column: "QoutationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_QoutationEvent_StaffId",
                table: "QoutationEvent",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_UserId",
                table: "Staff",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserObject_RoleId",
                table: "UserObject",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_AssignToId",
                schema: "Task",
                table: "Task",
                column: "AssignToId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_PriorityId",
                schema: "Task",
                table: "Task",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_TaskStatusId",
                schema: "Task",
                table: "Task",
                column: "TaskStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_WorkId",
                schema: "Task",
                table: "Task",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Work_PriorityId",
                schema: "Task",
                table: "Work",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Work_WorkStatusId",
                schema: "Task",
                table: "Work",
                column: "WorkStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CacheDatas");

            migrationBuilder.DropTable(
                name: "CommonConfigurations");

            migrationBuilder.DropTable(
                name: "Loggers");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "QoutationDetail");

            migrationBuilder.DropTable(
                name: "QoutationEvent");

            migrationBuilder.DropTable(
                name: "Task",
                schema: "Task");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Qoutation");

            migrationBuilder.DropTable(
                name: "TaskStatus",
                schema: "Task");

            migrationBuilder.DropTable(
                name: "Work",
                schema: "Task");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "QoutationStatus");

            migrationBuilder.DropTable(
                name: "Priority",
                schema: "Task");

            migrationBuilder.DropTable(
                name: "WorkStatus",
                schema: "Task");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserObject");

            migrationBuilder.DropTable(
                name: "AspNetRoles");
        }
    }
}
