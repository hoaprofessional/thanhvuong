using Framework.Context;
using Framework.Models.QoutationManagement;
using System;
namespace Framework.Repositories.QoutationManagement
{
    public interface IQuotesStatusWaitingApprovalInterestedRepository : IRepository<QuotesStatusWaitingApprovalInterested>
    {

    }
    public class QuotesStatusWaitingApprovalInterestedRepository : BaseRepository<QuotesStatusWaitingApprovalInterested>, IQuotesStatusWaitingApprovalInterestedRepository
    {
        public QuotesStatusWaitingApprovalInterestedRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }

        public override QuotesStatusWaitingApprovalInterested Add(QuotesStatusWaitingApprovalInterested entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            entity.Active = true;
            return base.Add(entity);
        }
        public override void Update(QuotesStatusWaitingApprovalInterested entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}