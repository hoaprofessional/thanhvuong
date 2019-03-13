using Framework.Models.QoutationManagement;
using WebCore.Areas.Admin.Models;
using WebCore.Services.Share.Admins.Orders.Dto;
using WebCore.Services.Share.Admins.Users.Dto;
using WebCore.Utils.ModelHelper;

namespace WebFramework.Areas.Management.Models
{
    public class OrderViewModel : AdminBaseViewModel
    {
        public PagingResultDto<Order> PagingResult { get; set; }
        public OrderFilterInput FilterInput { get; set; }
    }
}
