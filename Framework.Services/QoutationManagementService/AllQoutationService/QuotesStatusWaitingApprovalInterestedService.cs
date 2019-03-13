using System.Linq;
using Framework.DTOs.QoutationManagementDto.BaseOrderDto;
using Framework.DTOs.QoutationManagementDto.BaseQoutationDto;
using Framework.Repositories.QoutationManagement;
using Framework.Services.OrderManagementService.OrderListService;
using Framework.Services.QoutationManagementService.QoutationListService;
using Framework.Utils;

namespace Framework.Services.QoutationManagementService.AllQoutationService
{
    public interface IQuotesStatusWaitingApprovalInterestedService
        : IBaseQoutationService
    {

    }
    public class QuotesStatusWaitingApprovalInterestedService :
        BaseQoutationService,
        IQuotesStatusWaitingApprovalInterestedService
    {
        IQuotesStatusWaitingApprovalInterestedRepository quotesStatusWaitingApprovalInterestedRepository;
        IQoutationEventRepository qoutationEventRepository;
        public QuotesStatusWaitingApprovalInterestedService(
            IQoutationRepository QoutationRepository,
            IClientRepository clientRepository,
            IStaffRepository staffRepository,
            IQoutationStatusRepository qoutationStatusRepository,
            IQoutationEventRepository qoutationEventRepository,
            IQoutationDetailRepository QoutationDetailRepository,
            IQuotesStatusWaitingApprovalInterestedRepository quotesStatusWaitingApprovalInterestedRepository,
            IProductRepository productRepository) :
            base(QoutationRepository,
                clientRepository,
                staffRepository,
                qoutationStatusRepository,
                QoutationDetailRepository,
                productRepository)
        {
            this.quotesStatusWaitingApprovalInterestedRepository = quotesStatusWaitingApprovalInterestedRepository;
            this.qoutationEventRepository = qoutationEventRepository;
        }

        bool IsContain(string qoutationStatusId, 
            string currentStaffId,
            string[] permissions)
        {
            var qoutationEvents = qoutationEventRepository
                .GetMulti(x => x.StaffId == currentStaffId);

            var permissionFinder = "," + string.Join(",", permissions) + ",";

            var interesteds = quotesStatusWaitingApprovalInterestedRepository.
                GetMulti(x => permissionFinder.Contains("," + x.Permission + ","));

            return interesteds.Any(
                    interested =>
                    (interested.QoutationStatusId == qoutationStatusId)); 
        }

        protected override void InitConditionQuery(ref IQueryable<QoutationDto> query,
            string currentStaffId,
            string[] permissions)
        {
            if (currentStaffId == null || permissions == null)
                return;

            var qoutationEvents = qoutationEventRepository
                .GetMulti(x => x.StaffId == currentStaffId);

            var permissionFinder = "," + string.Join(",", permissions) + ",";
            
            var interesteds = quotesStatusWaitingApprovalInterestedRepository.
                GetMulti(x => permissionFinder.Contains("," + x.Permission + ","));


            query = query
                .Where(qoutation => interesteds.Any(
                    interested =>
                    (interested.QoutationStatusId == qoutation.QoutationQoutationStatusId) &&
                    (interested.IsSelf == false || (interested.IsSelf == true &&
                    qoutationEvents.Any(qoutationEvent => qoutationEvent.QoutationStatusId ==
                    interested.QoutationStatusStaffCreated && qoutationEvent.QoutationId == qoutation.QoutationId)
                    ))));
        }
    }
}
