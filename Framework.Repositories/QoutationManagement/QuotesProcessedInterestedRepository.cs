using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IQuotesProcessedInterestedRepository : IRepository<QuotesProcessedInterested>
    {

    }
    public class QuotesProcessedInterestedRepository : BaseRepository<QuotesProcessedInterested>, IQuotesProcessedInterestedRepository
    {
        public QuotesProcessedInterestedRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override QuotesProcessedInterested Add(QuotesProcessedInterested entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(QuotesProcessedInterested entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}