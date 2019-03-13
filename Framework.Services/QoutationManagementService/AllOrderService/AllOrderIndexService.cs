using Framework.DTOs.QoutationManagementDto.BaseOrderDto;
using Framework.Repositories.QoutationManagement;
using Framework.Services.OrderManagementService.OrderListService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.QoutationManagementService.AllOrderService
{
    public interface IAllOrderIndexService : IBaseOrderService
    {
        
    }
    public class AllOrderIndexService :
        BaseOrderService, IAllOrderIndexService
    {
        public AllOrderIndexService(IOrderRepository orderRepository, IClientRepository clientRepository, IStaffRepository staffRepository, IOrderStatusRepository orderStatusRepository, IOrderDetailRepository orderDetailRepository, IQoutationRepository qoutationRepository, IProductRepository productRepository) : base(orderRepository, clientRepository, staffRepository, orderStatusRepository, orderDetailRepository, qoutationRepository, productRepository)
        {
        }

        protected override void InitConditionQuery(ref IQueryable<OrderDto> query,
            string currentStaffId,
            string[] permissions)
        {
        }
    }
}
