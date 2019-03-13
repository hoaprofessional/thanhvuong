using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IQuotesStatusWaitingProcessInterestedRepository : IRepository<QuotesStatusWaitingProcessInterested>
    {

    }
    public class QuotesStatusWaitingProcessInterestedRepository : BaseRepository<QuotesStatusWaitingProcessInterested>, IQuotesStatusWaitingProcessInterestedRepository
    {
        public QuotesStatusWaitingProcessInterestedRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override QuotesStatusWaitingProcessInterested Add(QuotesStatusWaitingProcessInterested entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(QuotesStatusWaitingProcessInterested entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}