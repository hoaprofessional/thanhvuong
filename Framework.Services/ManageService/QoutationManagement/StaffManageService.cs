using Framework.Models.QoutationManagement;
using Framework.Repositories.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService.QoutationManagement
{
    public interface IStaffManageService : IManageServiceBase<Staff>
    {

    }
    public class StaffManageService : ManageServiceBase<Staff>, IStaffManageService
    {
        public StaffManageService(IStaffRepository repository) 
            : base(repository)
        {
        }
    }
}
