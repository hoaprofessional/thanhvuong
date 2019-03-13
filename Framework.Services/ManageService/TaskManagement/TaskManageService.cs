using Framework.Models.TaskManagement;
using Framework.Repositories.TaskManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService.TaskManagement
{
    public interface ITaskManageService : IManageServiceBase<Task>
    {

    }
    public class TaskManageService : ManageServiceBase<Task>, ITaskManageService
    {
        public TaskManageService(ITaskRepository repository) 
            : base(repository)
        {
        }
    }
}
