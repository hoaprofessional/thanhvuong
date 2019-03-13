using Framework.Models.UserManagement;
using Framework.Utils.Anotations.DtoAnotation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.WorkManagementDto.WorkList.WorkListIndex
{
    public class CreateByFilterDto : IRef<ApplicationUser>
    {
        [MapColumn("ApplicationUserUserName")]
        public String UserName { get; set; }
        [MapColumn("ApplicationUserName")]
        public String Name { get; set; }
    }
}
