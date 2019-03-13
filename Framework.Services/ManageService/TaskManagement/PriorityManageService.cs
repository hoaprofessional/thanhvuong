using Framework.Models.TaskManagement;
using Framework.Repositories.TaskManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService.TaskManagement
{
    public interface IPriorityManageService : IManageServiceBase<Priority>
    {

    }
    public class PriorityManageService : ManageServiceBase<Priority>, IPriorityManageService
    {
        public PriorityManageService(IPriorityRepository repository)
            : base(repository)
        {
        }
    }
}
