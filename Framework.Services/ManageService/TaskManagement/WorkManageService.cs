using Framework.Models.TaskManagement;
using Framework.Repositories.TaskManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService
{
    public interface IWorkManageService : IManageServiceBase<Work>
    {

    }
    public class WorkManageService : ManageServiceBase<Work>, IWorkManageService
    {
        public WorkManageService(IWorkRepository repository) 
            : base(repository)
        {
        }
    }
}
