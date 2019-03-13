using Framework.Repositories.Configuration;
using Framework.Repositories.Infrastructor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.Shared
{
    public interface IIdConfigurationService
    {
        String GenerateNewId(String prefix);
    }
    public class IdConfigurationService : IIdConfigurationService
    {
        IIdConfigurationRepository idConfigurationRepository;
        public IdConfigurationService(IIdConfigurationRepository idConfigurationRepository,
            IUnitOfWork unitOfWork)
        {
            this.idConfigurationRepository = idConfigurationRepository;
        }
        public String GenerateNewId(String prefix)
        {
            return idConfigurationRepository.GenerateNewId(prefix);
        }
    }
}
