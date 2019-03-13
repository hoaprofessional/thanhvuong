using Framework.Models.TaskManagement;
using Framework.Repositories.TaskManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService.TaskManagement
{
    public interface ITaskStatusManageService : IManageServiceBase<TaskStatus>
    {

    }
    public class TaskStatusManageService : ManageServiceBase<TaskStatus>, ITaskStatusManageService
    {
        public TaskStatusManageService(ITaskStatusRepository repository) 
            : base(repository)
        {
        }
    }
}
