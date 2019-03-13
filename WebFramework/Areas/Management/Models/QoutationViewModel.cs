using Framework.Models.QoutationManagement;
using WebCore.Areas.Admin.Models;
using WebCore.Services.Share.Admins.Qoutations.Dto;
using WebCore.Services.Share.Admins.Users.Dto;
using WebCore.Utils.ModelHelper;

namespace WebFramework.Areas.Management.Models
{
    public class QoutationViewModel : AdminBaseViewModel
    {
        public PagingResultDto<Qoutation> PagingResult { get; set; }
        public QoutationFilterInput FilterInput { get; set; }
    }
}
