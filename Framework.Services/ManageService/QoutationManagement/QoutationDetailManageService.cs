using Framework.Models.QoutationManagement;
using Framework.Repositories.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService.QoutationManagement
{
    public interface IQoutationDetailManageService : IManageServiceBase<QoutationDetail>
    {

    }
    public class QoutationDetailManageService : ManageServiceBase<QoutationDetail>, IQoutationDetailManageService
    {
        public QoutationDetailManageService(IQoutationDetailRepository repository) 
            : base(repository)
        {
        }
    }
}
