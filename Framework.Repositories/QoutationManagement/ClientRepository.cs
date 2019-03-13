using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IClientRepository : IRepository<Client>
    {
        
    }
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override Client Add(Client entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(Client entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}