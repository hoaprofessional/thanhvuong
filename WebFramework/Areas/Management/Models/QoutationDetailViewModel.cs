using Framework.Models.QoutationManagement;
using WebCore.Areas.Admin.Models;
using WebCore.Services.Share.Admins.QoutationDetails.Dto;
using WebCore.Services.Share.Admins.Qoutations.Dto;
using WebCore.Services.Share.Admins.Users.Dto;
using WebCore.Utils.ModelHelper;

namespace WebFramework.Areas.Management.Models
{
    public class QoutationDetailViewModel : AdminBaseViewModel
    {
        public PagingResultDto<QoutationDetail> PagingResult { get; set; }
        public QoutationDetailFilterInput FilterInput { get; set; }
    }
}
