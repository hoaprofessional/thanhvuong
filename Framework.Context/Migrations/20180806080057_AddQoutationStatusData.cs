using Framework.Utils;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Framework.Context.Migrations
{
    public partial class AddQoutationStatusData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("QoutationStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { QoutationStatusIdHelper.AlreadyCreated,
                    "1", "Vừa được tạo", "Vừa được nhân viên sale tạo khi gặp khách hàng, chờ trưởng phòng sale phê duyệt, Người tạo: Nhân viên sale, Người được thông báo: Trưởng phòng sale" });

            migrationBuilder.InsertData("QoutationStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { QoutationStatusIdHelper.SalesManagerApproveSalesStaff,
                    "1", "Đã duyệt đơn", "Trưởng phòng sale đã phê duyệt báo giá từ nhân viên sale, đang đợi nhân viên kế toán điền giá, Người tạo: Trưởng phòng sale, Người được thông báo: Nhân viên kế toán" });

            migrationBuilder.InsertData("QoutationStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { QoutationStatusIdHelper.AccountantFilledPriceBuy,
                    "1", "Đã điền giá", "Nhân viên kế toán đã điền giá, đang đợi sự phê duyệt của trưởng phòng kế toán, Người tạo: Nhân viên kế toán, Người được thông báo: Nhân viên kế toán, Trưởng phòng kế toán" });

            migrationBuilder.InsertData("QoutationStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { QoutationStatusIdHelper.AccountingManagerApprovedPrice,
                    "1", "Đã duyệt giá : Trưởng phòng kế toán", "Trưởng phòng kế toán đã phê duyệt báo giá, đang đợi sự phê duyệt của trưởng phòng sale, Người tạo: Trưởng phòng kế toán, Người được thông báo: Trưởng phòng sale" });

            migrationBuilder.InsertData("QoutationStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { QoutationStatusIdHelper.AccountingManagerRejectApprovedPrice,
                    "1", "Từ chối duyệt giá : Trưởng phòng kế toán", "Trưởng phòng kế toán từ chối phê duyệt báo giá, đang đợi nhân viên kế toán điền lại giá, Người tạo: Trưởng phòng kế toán, Người được thông báo: Nhân viên kế toán" });

            migrationBuilder.InsertData("QoutationStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { QoutationStatusIdHelper.SalesManagerApproveAccountingDepartment,
                    "1", "Đã duyệt giá : Trưởng phòng sale", "Trưởng phòng sale đã phê duyệt báo giá từ phòng kế toán và lúc này hệ thống sẽ tạo ra file báo giá để nhân viên sale đi in, Người tạo: Trưởng phòng sale, Người được thông báo: Nhân viên sale" });

            migrationBuilder.InsertData("QoutationStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { QoutationStatusIdHelper.SalesManagerRejectPriceAccountingDepartment,
                    "1", "Từ chối duyệt giá : Trưởng phòng sale", "Trưởng phòng sale không phê duyệt báo giá từ phòng kế toán, đang đợi nhân viên kế toán điền lại giá, Người tạo: Trưởng phòng sale, Người được thông báo: Nhân viên kế toán" });

            migrationBuilder.InsertData("QoutationStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { QoutationStatusIdHelper.ClientAccepted, "1", "Khách hàng đồng ý báo giá", "Khách hàng đã đồng ý báo giá của khi nhân viên sale đem báo giá có thể tạo đơn đặt hàng, Người tạo: Nhân viên sale" });

            migrationBuilder.InsertData("QoutationStatus"
                , new string[] { "Id", "Active", "Name", "Note" }
                , new string[] { QoutationStatusIdHelper.ClientRejected, "1", "Khách hàng từ chối báo giá", "Khách hàng không đồng ý điều kiện của hóa đơn mà nhân viên sale gửi. Trong trường hợp này sẽ quay lại chờ trưởng phòng sale phê duyệt kèm lí do, Người tạo: Nhân viên sale, Người được thông báo: Trưởng phòng sale" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
