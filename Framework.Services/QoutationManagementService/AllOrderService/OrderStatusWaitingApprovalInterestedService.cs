using System.Linq;
using Framework.DTOs.QoutationManagementDto.BaseOrderDto;
using Framework.DTOs.QoutationManagementDto.BaseQoutationDto;
using Framework.Repositories.QoutationManagement;
using Framework.Services.OrderManagementService.OrderListService;
using Framework.Services.QoutationManagementService.QoutationListService;
using Framework.Utils;

namespace Framework.Services.QoutationManagementService.AllOrderService
{
    public interface IOrderStatusWaitingApprovalInterestedService
        : IBaseOrderService
    {

    }
    public class OrderStatusWaitingApprovalInterestedService :
        BaseOrderService,
        IOrderStatusWaitingApprovalInterestedService
    {
        IOrderStatusWaitingApprovalInterestedRepository orderStatusWaitingApprovalInterestedRepository;
        IOrderEventRepository orderEventRepository;
        public OrderStatusWaitingApprovalInterestedService(
            IOrderRepository orderRepository,
            IClientRepository clientRepository,
            IStaffRepository staffRepository,
            IOrderStatusRepository orderStatusRepository,
            IOrderEventRepository orderEventRepository,
            IQoutationRepository qoutationRepository,
            IOrderDetailRepository orderDetailRepository,
            IOrderStatusWaitingApprovalInterestedRepository orderStatusWaitingApprovalInterestedRepository,
            IProductRepository productRepository) :
            base(orderRepository,
                clientRepository,
                staffRepository,
                orderStatusRepository,
                orderDetailRepository,
                qoutationRepository,
                productRepository)
        {
            this.orderStatusWaitingApprovalInterestedRepository = orderStatusWaitingApprovalInterestedRepository;
            this.orderEventRepository = orderEventRepository;
        }

        protected override void InitConditionQuery(ref IQueryable<OrderDto> query,
            string currentStaffId,
            string[] permissions)
        {
            if (currentStaffId == null || permissions == null)
                return;

            var orderEvents = orderEventRepository
                .GetMulti(x => x.StaffId == currentStaffId);

            var permissionFinder = "," + string.Join(",", permissions) + ",";
            
            var interesteds = orderStatusWaitingApprovalInterestedRepository.
                GetMulti(x => permissionFinder.Contains("," + x.Permission + ","));

            query = query
                .Where(order => interesteds.Any(
                    interested =>
                    (interested.OrderStatusId == order.OrderOrderStatusId) &&
                    (interested.IsSelf == false || (interested.IsSelf == true &&
                    orderEvents.Any(orderEvent => orderEvent.OrderStatusId ==
                    interested.OrderStatusStaffCreated && orderEvent.OrderId == order.OrderId)
                    ))));
        }


    }
}
