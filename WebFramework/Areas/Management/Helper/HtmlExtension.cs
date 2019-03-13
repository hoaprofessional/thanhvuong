using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebFramework.Areas.Management.Helper
{
    public static class HtmlExtension
    {
        public static Dictionary<string, string> dictionaries = null;
        public static IHtmlContent DictionaryFor(this IHtmlHelper htmlHelper, string key)
        {
            if(dictionaries==null)
            {
                SetValueForDictionary();
            }
            if (!dictionaries.Keys.Contains(key))
            {
                return htmlHelper.Raw("[" + key + "]");
            }
            return htmlHelper.Raw(dictionaries[key]);
        }

        private static void SetValueForDictionary()
        {
            dictionaries = new Dictionary<string, string>();
            dictionaries.Add("RegisterUser", "Đăng ký user");
            dictionaries.Add("AssignTask", "Giao task");
            dictionaries.Add("AdjustPriceBuy", "Điều chỉnh giá sản phẩm báo giá mua");
            dictionaries.Add("AdjustPriceSell", "Điều chỉnh giá sản phẩm báo giá bán");
            dictionaries.Add("AccountingManagerApprovePrice", "Trưởng phòng kế toán phê duyệt giá báo giá");
            dictionaries.Add("SalesManagerApprovePrice", "Trưởng phòng kinh doanh phê duyệt giá báo giá");
            dictionaries.Add("ApproveQoutation", "Phê duyệt báo giá vừa tạo");
            dictionaries.Add("QuotesPrice", "Báo giá");
            dictionaries.Add("CanCreateOrder", "Tạo đơn hàng");
            dictionaries.Add("RejectQoutation", "Hủy báo giá");
            dictionaries.Add("CreateQoutation", "Tạo báo giá");
            dictionaries.Add("CanApproveAlreadyCreatedOrder", "Phê duyệt đơn hàng vừa tạo");
            dictionaries.Add("CanAccountantHasOrdered", "Đặt hàng cho đơn hàng");
            dictionaries.Add("AccountingManagerApproveOrder", "Trưởng phòng kế toán phê duyệt đơn hàng");
            dictionaries.Add("CanUpdateGoodOnWay", "Cập nhật tình trạng hàng đang trên đường về");
            dictionaries.Add("CanUpdateReadyToDeliver", "Cập nhật tình trạng hàng sẵn sàng giao");
            dictionaries.Add("CanRecommendedDelivery", "Cập nhật tình trạng đề nghị giao hàng");
            dictionaries.Add("CanTechnicalChiefApproveOrder", "Trưởng phòng tổng hợp tiếp nhận đơn hàng");
            dictionaries.Add("CanFinishDelivery", "Cập nhật tình trạng hoàn thành lắp đặt");
            dictionaries.Add("ConfirmOrder", "Giám đốc xác nhận đơn hàng");
            dictionaries.Add("CanReceiveMoney", "Thu tiền đơn hàng");
            dictionaries.Add("UserManagement", "Quản trị người dùng");
            dictionaries.Add("UserManagement_BlockUser", "Khóa tài khoản");
            dictionaries.Add("UserManagement_AssignPermission", "Phân quyền");

            dictionaries.Add("accountant_buy", "Nhân viên kế toán nhập");
            dictionaries.Add("accountant_sell", "Nhân viên kế toán bán");
            dictionaries.Add("accounting_manager", "Trưởng phòng kế toán");
            dictionaries.Add("admin", "Quản trị hệ thống");
            dictionaries.Add("chief_technical", "Trưởng phòng tổng hợp");
            dictionaries.Add("manager", "Giám đốc");
            dictionaries.Add("sale_staff", "Nhân viên sale");
            dictionaries.Add("sales_manager", "Trưởng phòng sale");
        }
    }
}
