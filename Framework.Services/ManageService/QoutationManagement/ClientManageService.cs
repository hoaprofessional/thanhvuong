using Framework.Models.QoutationManagement;
using Framework.Repositories.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService.QoutationManagement
{
    public interface IClientManageService : IManageServiceBase<Client>
    {

    }
    public class ClientManageService : ManageServiceBase<Client>, IClientManageService
    {
        public ClientManageService(IClientRepository repository) 
            : base(repository)
        {
        }
    }
}
