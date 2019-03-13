using Framework.Models.TaskManagement;
using Framework.Repositories.TaskManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService.TaskManagement
{
    public interface IWorkStatusManageService : IManageServiceBase<WorkStatus>
    {

    }
    public class WorkStatusManageService : ManageServiceBase<WorkStatus>, IWorkStatusManageService
    {
        public WorkStatusManageService(IWorkStatusRepository repository) 
            : base(repository)
        {
        }
    }
}
