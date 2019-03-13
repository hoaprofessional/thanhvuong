using System.Linq;
using Framework.DTOs.QoutationManagementDto.BaseOrderDto;
using Framework.DTOs.QoutationManagementDto.BaseQoutationDto;
using Framework.Repositories.QoutationManagement;
using Framework.Services.OrderManagementService.OrderListService;
using Framework.Services.QoutationManagementService.QoutationListService;
using Framework.Utils;

namespace Framework.Services.QoutationManagementService.AllQoutationService
{
    public interface IQuotesProcessedInterestedService
        : IBaseQoutationService
    {

    }
    public class QuotesProcessedInterestedService :
        BaseQoutationService,
        IQuotesProcessedInterestedService
    {
        IQuotesProcessedInterestedRepository quotesProcessedInterestedRepository;
        IQoutationEventRepository qoutationEventRepository;
        public QuotesProcessedInterestedService(
            IQoutationRepository QoutationRepository,
            IClientRepository clientRepository,
            IStaffRepository staffRepository,
            IQoutationStatusRepository qoutationStatusRepository,
            IQoutationEventRepository qoutationEventRepository,
            IQoutationDetailRepository QoutationDetailRepository,
            IQuotesProcessedInterestedRepository quotesProcessedInterestedRepository,
            IProductRepository productRepository) :
            base(QoutationRepository,
                clientRepository,
                staffRepository,
                qoutationStatusRepository,
                QoutationDetailRepository,
                productRepository)
        {
            this.quotesProcessedInterestedRepository = quotesProcessedInterestedRepository;
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
            
            var interesteds = quotesProcessedInterestedRepository.
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
