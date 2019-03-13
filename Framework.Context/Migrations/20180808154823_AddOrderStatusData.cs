using Framework.Utils;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddOrderStatusData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("OrderStatus"
    , new string[] { "Id", "Active", "Name", "Note" }
    , new string[] { OrderStatusIdHelper.AlreadyCreated,
                    "1", "Vừa được tạo", "Vừa được nhân viên sale tạo khi gặp khách hàng, chờ trưởng phòng sale phê duyệt, Người tạo: Nhân viên sale, Người được thông báo: Trưởng phòng sale" });

            migrationBuilder.InsertData("OrderStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { OrderStatusIdHelper.SalesManagerApprove,
                    "1", "Đã duyệt đơn : Trưởng phòng sale", "Trưởng phòng sale đã phê duyệt đơn đặt hàng từ nhân viên sale, đang đợi nhân viên kế toán đặt hàng, Người tạo: Trưởng phòng sale, Người được thông báo: Nhân viên kế toán" });

            migrationBuilder.InsertData("OrderStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { OrderStatusIdHelper.SalesManagerReject,
                    "1", "Từ chối : Trưởng phòng sale", "Trưởng phòng sale đã từ chối phê duyệt đơn đặt hàng từ nhân viên sale, Người tạo: Trưởng phòng sale, Người được thông báo: Nhân viên sale" });

            migrationBuilder.InsertData("OrderStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { OrderStatusIdHelper.AccountantHasOrdered,
                    "1", "Đã đặt hàng", "Nhân viên kế toán đã đặt hàng, Người tạo: Nhân viên kế toán" });

            migrationBuilder.InsertData("OrderStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { OrderStatusIdHelper.GoodOnWay,
                    "1", "Hàng đang trên đường về", "Hàng đang trên đường về, Người tạo: Nhân viên kế toán" });

            migrationBuilder.InsertData("OrderStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { OrderStatusIdHelper.ReadyToDeliver,
                    "1", "Hàng sẵn sàng giao", "Hàng sẵn sàng giao, Người tạo: Nhân viên kế toán" });

            migrationBuilder.InsertData("OrderStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { OrderStatusIdHelper.ChiefTechnicalApprove,
                    "1", "Đã duyệt đơn : Trưởng phòng kỹ thuật", "Trưởng phòng kỹ thuật tiếp nhận đơn, Người tạo: Trưởng phòng kỹ thuật" });

            migrationBuilder.InsertData("OrderStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { OrderStatusIdHelper.ChiefTechnicalDeliver,
                    "1", "Đã giao hàng", "Trưởng phòng kỹ thuật đã giao hàng chờ nhân viên kế toán thu tiền, Người tạo: Trưởng phòng kỹ thuật, Người được thông báo: Nhân viên kế toán" });

            migrationBuilder.InsertData("OrderStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { OrderStatusIdHelper.AccountantReceiveMoney,
                    "1", "Đã thu tiền", "Nhân viên kế toán đã thu tiền, Người tạo: Nhân viên kế toán" });

            migrationBuilder.InsertData("OrderStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { OrderStatusIdHelper.ClientDept,
                    "1", "Khách hàng nợ", "Nhân viên kế toán đã thu tiền nhưng khách hàng nợ, Người tạo: Nhân viên kế toán" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
