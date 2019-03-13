using Framework.Context;
using Framework.Models;
using Framework.Repositories.Infrastructor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repositories.Configuration
{
    public interface IIdConfigurationRepository : IRepository<IdConfiguration>
    {
        String GenerateNewId(string prefix);
    }

    public class IdConfigurationRepository : BaseRepository<IdConfiguration>,
        IIdConfigurationRepository
    {
        IUnitOfWork unitOfWork;
        public IdConfigurationRepository(FrameworkDbContext dataContext,
            IUnitOfWork unitOfWork) : base(dataContext)
        {
            this.unitOfWork = unitOfWork;
        }

        public string GenerateNewId(string prefix)
        {
            var id = GetSingleByCondition(x => x.Prefix == prefix);
            if(id==null)
            {
                id = new IdConfiguration()
                {
                    Prefix = prefix,
                    CurrentValue = 0
                };
                Add(id);
                unitOfWork.Commit();
            }
            id.CurrentValue++;
            Update(id);

            unitOfWork.Commit();

            return prefix + String.Format("{0:000}", id.CurrentValue);

        }
    }
}
