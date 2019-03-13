using System.Linq;
using Framework.DTOs.QoutationManagementDto.BaseOrderDto;
using Framework.DTOs.QoutationManagementDto.BaseQoutationDto;
using Framework.Repositories.QoutationManagement;
using Framework.Services.OrderManagementService.OrderListService;
using Framework.Services.QoutationManagementService.QoutationListService;
using Framework.Utils;

namespace Framework.Services.QoutationManagementService.AllQoutationService
{
    public interface IQuotesStatusWaitingProcessInterestedService
        : IBaseQoutationService
    {

    }
    public class QuotesStatusWaitingProcessInterestedService :
        BaseQoutationService,
        IQuotesStatusWaitingProcessInterestedService
    {
        IQuotesStatusWaitingProcessInterestedRepository quotesStatusWaitingProcessInterestedRepository;
        IQoutationEventRepository qoutationEventRepository;
        public QuotesStatusWaitingProcessInterestedService(
            IQoutationRepository QoutationRepository,
            IClientRepository clientRepository,
            IStaffRepository staffRepository,
            IQoutationStatusRepository qoutationStatusRepository,
            IQoutationEventRepository qoutationEventRepository,
            IQoutationDetailRepository QoutationDetailRepository,
            IQuotesStatusWaitingProcessInterestedRepository quotesStatusWaitingProcessInterestedRepository,
            IProductRepository productRepository) :
            base(QoutationRepository,
                clientRepository,
                staffRepository,
                qoutationStatusRepository,
                QoutationDetailRepository,
                productRepository)
        {
            this.quotesStatusWaitingProcessInterestedRepository = quotesStatusWaitingProcessInterestedRepository;
            this.qoutationEventRepository = qoutationEventRepository;
        }

        protected override void InitConditionQuery(ref IQueryable<QoutationDto> query,
            string currentStaffId,
            string[] permissions)
        {
            if (currentStaffId == null || permissions == null)
                return;

            var qoutationEvents = qoutationEventRepository.GetMulti(x => x.StaffId == currentStaffId);

            var permissionFinder = "," + string.Join(",", permissions) + ",";
            
            var interesteds = quotesStatusWaitingProcessInterestedRepository.
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
