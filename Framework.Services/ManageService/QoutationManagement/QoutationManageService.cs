using Framework.Models.QoutationManagement;
using Framework.Repositories.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService.QoutationManagement
{
    public interface IQoutationManageService : IManageServiceBase<Qoutation>
    {
        Qoutation GetById(int id);
    }
    public class QoutationManageService : ManageServiceBase<Qoutation>, IQoutationManageService
    {
        public QoutationManageService(IQoutationRepository repository) 
            : base(repository)
        {
        }

        public Qoutation GetById(int id)
        {
            return repository.GetSingleByCondition(x => x.Id == id);
        }
    }
}
