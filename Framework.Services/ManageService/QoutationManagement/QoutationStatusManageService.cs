using Framework.Models.QoutationManagement;
using Framework.Repositories.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService.QoutationManagement
{
    public interface IQoutationStatusManageService : IManageServiceBase<QoutationStatus>
    {

    }
    public class QoutationStatusManageService : ManageServiceBase<QoutationStatus>, IQoutationStatusManageService
    {
        public QoutationStatusManageService(IQoutationStatusRepository repository) 
            : base(repository)
        {
        }
    }
}
