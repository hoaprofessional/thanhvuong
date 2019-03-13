using Framework.Models.QoutationManagement;
using WebCore.Areas.Admin.Models;
using WebCore.Services.Share.Admins.OrderDetails.Dto;
using WebCore.Services.Share.Admins.Orders.Dto;
using WebCore.Services.Share.Admins.Users.Dto;
using WebCore.Utils.ModelHelper;

namespace WebFramework.Areas.Management.Models
{
    public class OrderDetailViewModel : AdminBaseViewModel
    {
        public PagingResultDto<OrderDetail> PagingResult { get; set; }
        public OrderDetailFilterInput FilterInput { get; set; }
    }
}
