using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFramework.Infrastructor
{
    public static class PermissionValue
    {
        public const string RegisterUser="RegisterUser";
        public const string AssignTask = "AssignTask";
        public const string ViewReport1 = "ViewReport1";
        public const string ViewReport2 = "ViewReport2";
        public const string ViewReport3 = "ViewReport3";
        public const string ViewReport4 = "ViewReport4";
        public const string ViewReport5 = "ViewReport5";
        public const string AdjustPriceBuy = "AdjustPriceBuy";
        public const string AdjustPriceSell = "AdjustPriceSell";
        public const string AccountingManagerApprovePrice = "AccountingManagerApprovePrice";
        public const string SalesManagerApprovePrice = "SalesManagerApprovePrice";
        public const string ApproveQoutation = "ApproveQoutation";
        public const string QuotesPrice = "QuotesPrice";
        public const string CanCreateOrder = "CanCreateOrder";
        public const string RejectQoutation = "RejectQoutation";
        public const string CreateQoutation = "CreateQoutation";
        public const string CanApproveAlreadyCreatedOrder = "CanApproveAlreadyCreatedOrder";
        public const string CanAccountantHasOrdered = "CanAccountantHasOrdered";
        public const string AccountingManagerApproveOrder = "AccountingManagerApproveOrder";
        public const string CanUpdateGoodOnWay = "CanUpdateGoodOnWay";
        public const string CanUpdateReadyToDeliver = "CanUpdateReadyToDeliver";
        public const string CanRecommendedDelivery = "CanRecommendedDelivery";
        public const string CanTechnicalChiefApproveOrder = "CanTechnicalChiefApproveOrder";
        public const string CanFinishDelivery = "CanFinishDelivery";
        public const string ConfirmOrder = "ConfirmOrder";
        public const string CanReceiveMoney = "CanReceiveMoney";


        public const string UserManagement = "UserManagement";
        public const string UserManagement_BlockUser = "UserManagement.BlockUser";
        public const string UserManagement_AssignPermission = "UserManagement.AssignPermission";
    }
}
