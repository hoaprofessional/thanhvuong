using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Areas.Admin.Models;
using WebCore.Services.Share.Admins.Roles.Dto;
using WebCore.Utils.ModelHelper;
using WebCore.Utils.TreeViewHelper;

namespace WebFramework.Areas.Management.Models
{
    public class RoleViewModel : AdminBaseViewModel
    {
        public PagingResultDto<RoleDto> PagingResult { get; set; }
        public RoleFilterInput FilterInput { get; set; }
    }
}
