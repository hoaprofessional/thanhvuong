using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("AspNetRoles", new string[]
            {
                "Id",
                "Name",
                "NormalizedName"
            },
            new string[]
            {
                Resource1.AccountantId,
                Resource1.AccountantId,
                Resource1.AccountantId
            });
            migrationBuilder.InsertData("AspNetRoles", new string[]
            {
                "Id",
                "Name",
                "NormalizedName"
            },
            new string[]
            {
                Resource1.AdminId,
                Resource1.AdminId,
                Resource1.AdminId
            });
            migrationBuilder.InsertData("AspNetRoles", new string[]
            {
                "Id",
                "Name",
                "NormalizedName"
            },
            new string[]
            {
                Resource1.SaleStaffId,
                Resource1.SaleStaffId,
                Resource1.SaleStaffId
            });

            migrationBuilder.InsertData("AspNetRoles", new string[]
            {
                "Id",
                "Name",
                "NormalizedName"
            },
            new string[]
            {
                Resource1.SalesManagerId,
                Resource1.SalesManagerId,
                Resource1.SalesManagerId
            });

            migrationBuilder.InsertData("AspNetRoles", new string[]
            {
                "Id",
                "Name",
                "NormalizedName"
            },
            new string[]
            {
                Resource1.AccountingManagerId,
                Resource1.AccountingManagerId,
                Resource1.AccountingManagerId
            });

            migrationBuilder.InsertData("AspNetRoles", new string[]
            {
                "Id",
                "Name",
                "NormalizedName"
            },
            new string[]
            {
                Resource1.ManagerId,
                Resource1.ManagerId,
                Resource1.ManagerId
            });

            migrationBuilder.InsertData("AspNetRoles", new string[]
            {
                "Id",
                "Name",
                "NormalizedName"
            },
            new string[]
            {
                Resource1.ChiefTechnicalId,
                Resource1.ChiefTechnicalId,
                Resource1.ChiefTechnicalId
            });


            migrationBuilder.InsertData("UserObject", new string[]
            {
                "Id",
                "DefaultCarrer",
                "Name",
                "RoleId"
            },
            new string[]
            {
                Resource1.SaleStaffId,
                Resource1.SaleStaffName,
                Resource1.SaleStaffName,
                Resource1.SaleStaffId
            });

            migrationBuilder.InsertData("UserObject", new string[]
            {
                "Id",
                "DefaultCarrer",
                "Name",
                "RoleId"
            },
            new string[]
            {
                Resource1.SalesManagerId,
                Resource1.SalesManagerName,
                Resource1.SalesManagerName,
                Resource1.SalesManagerId
            });

            migrationBuilder.InsertData("UserObject", new string[]
            {
                "Id",
                "DefaultCarrer",
                "Name",
                "RoleId"
            },
            new string[]
            {
                Resource1.AccountingManagerId,
                Resource1.AccountingManagerName,
                Resource1.AccountingManagerName,
                Resource1.AccountingManagerId
            });

            migrationBuilder.InsertData("UserObject", new string[]
            {
                "Id",
                "DefaultCarrer",
                "Name",
                "RoleId"
            },
            new string[]
            {
                Resource1.AdminId,
                Resource1.AdminName,
                Resource1.AdminName,
                Resource1.AdminId
            });

            migrationBuilder.InsertData("UserObject", new string[]
            {
                "Id",
                "DefaultCarrer",
                "Name",
                "RoleId"
            },
            new string[]
            {
                Resource1.AccountantId,
                Resource1.AccountantName,
                Resource1.AccountantName,
                Resource1.AccountantId
            });

            migrationBuilder.InsertData("UserObject", new string[]
            {
                "Id",
                "DefaultCarrer",
                "Name",
                "RoleId"
            },
            new string[]
            {
                Resource1.ChiefTechnicalId,
                Resource1.ChiefTechnicalName,
                Resource1.ChiefTechnicalName,
                Resource1.ChiefTechnicalId
            });

            migrationBuilder.InsertData("UserObject", new string[]
            {
                "Id",
                "DefaultCarrer",
                "Name",
                "RoleId"
            },
            new string[]
            {
                Resource1.ManagerId,
                Resource1.ManagerName,
                Resource1.ManagerName,
                Resource1.ManagerId
            });

            migrationBuilder.Sql(@"INSERT [dbo].[AspNetRoleClaims] ([ClaimType], [ClaimValue], [RoleId]) VALUES (N'Permission', N'RegisterUser', N'admin')
INSERT[dbo].[AspNetRoleClaims]([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'AssignTask', N'manager')
INSERT[dbo].[AspNetRoleClaims]([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'AssignTask', N'accounting_manager')
INSERT[dbo].[AspNetRoleClaims] ([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'AssignTask', N'sales_manager')
INSERT[dbo].[AspNetRoleClaims] ([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'ViewReport3', N'manager')
INSERT[dbo].[AspNetRoleClaims] ([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'ViewReport4', N'manager')
INSERT[dbo].[AspNetRoleClaims] ([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'ViewReport5', N'manager')
INSERT[dbo].[AspNetRoleClaims] ([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'QuotesPrice', N'sale_staff')
INSERT[dbo].[AspNetRoleClaims] ([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'ApproveQoutation', N'sales_manager')
INSERT[dbo].[AspNetRoleClaims] ([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'RejectQoutation', N'sales_manager')
INSERT[dbo].[AspNetRoleClaims] ([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'AdjustPrice', N'accountant')
INSERT[dbo].[AspNetRoleClaims] ([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'CreateQoutation', N'sale_staff')
INSERT[dbo].[AspNetRoleClaims] ([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'AccountingManagerApprovePrice', N'accounting_manager')
INSERT[dbo].[AspNetRoleClaims] ([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'SalesManagerApprovePrice', N'sales_manager')
INSERT[dbo].[AspNetRoleClaims] ([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'CanCreateOrder', N'sale_staff')
INSERT[dbo].[AspNetRoleClaims] ([ClaimType], [ClaimValue], [RoleId]) VALUES(N'Permission', N'QuotesPrice', N'sale_staff')", true);

            migrationBuilder.Sql("INSERT[dbo].[CommonConfigurations]([Id], [Active], [CompanyName], [Contact], [CreationTime], [CreationUserName], [HeadQuarter], [Hotline], [IsTest], [Logo], [ModifiedTime], [ModifiedUserName], [Note], [Protected], [Website]) VALUES(N'c1', 1, N'TNHH Thương Mại Dịch Vụ Thanh Vương', N'0258.3551322 hoặc 0258.3550768', NULL, NULL, N'Số 22 Điện Biên Phủ, P. Vĩnh Hòa, TP. Nha Trang', N'0905.154.468 - 0905.059.969', NULL, N'/images/company.png', NULL, NULL, NULL, NULL, N'thanhvuongco.com')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
