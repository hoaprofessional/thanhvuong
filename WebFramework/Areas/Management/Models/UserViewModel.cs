using WebCore.Areas.Admin.Models;
using WebCore.Services.Share.Admins.Users.Dto;
using WebCore.Utils.ModelHelper;

namespace WebFramework.Areas.Management.Models
{
    public class UserViewModel : AdminBaseViewModel
    {
        public PagingResultDto<UserDto> PagingResult { get; set; }
        public UserFilterInput FilterInput { get; set; }
    }
}
