using Framework.Context;
using Framework.Models.Configuration;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.Configuration
{
    public interface ICommonConfigurationRepository : IRepository<CommonConfiguration>
    {

    }
    public class CommonConfigurationRepository : BaseRepository<CommonConfiguration>, ICommonConfigurationRepository
    {
        public CommonConfigurationRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override CommonConfiguration Add(CommonConfiguration entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            if (entity.IsTest != true)
                entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(CommonConfiguration entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}