using Framework.Models.Configuration;
using Framework.Models.QoutationManagement;
using WebFramework.Models.Shared;

namespace WebFramework.Models.QoutationManagementViewModels.OrderDetailViewModel
{
    public class OrderDetailIndexViewModel : LayoutViewModel
    {
        public Order Order { get; set; }
        public CommonConfiguration CommonConfiguration { get; set; }
        public bool CanApproveAlreadyCreatedOrder { get; set; }
        public bool CanAccountantHasOrdered { get; set; }
        public bool CanChangeQuantityProduct { get; set; }
        public bool CanChangeExpectedReturnGoodDate { get; set; }
        public bool CanConfirmOrder { get; set; }
        public bool CanAccountingManagerApprove { get; set; }
        public bool CanUpdateGoodOnWay { get; set; }
        public bool CanUpdateReadyToDeliver { get; set; }
        public bool CanRecommendedDelivery { get; set; }
        public bool CanTechnicalChiefApproveOrder { get; set; }
        public bool CanFinishDelivery { get; set; }
        public bool CanReceiveMoney { get; set; }
    }
}
