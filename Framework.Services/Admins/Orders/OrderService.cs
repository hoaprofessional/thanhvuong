namespace WebCore.Services.Share.Admins.Orders
{
    using AutoMapper;
    using Framework.Models.QoutationManagement;
    using Framework.Repositories.QoutationManagement;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using WebCore.Services.Impl.Commons;
    using WebCore.Services.Share.Admins.Orders.Dto;
    using WebCore.Utils.CollectionHelper;
    using WebCore.Utils.FilterHelper;
    using WebCore.Utils.ModelHelper;

    public interface IOrderService
    {
        PagingResultDto<Order> GetAllByPaging(OrderFilterInput filterInput);
        void DeleteOrder(string orderId);
    }

    public class OrderService : BaseWebService, IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;



        public OrderService(IOrderRepository qoutationRepository, IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            orderRepository = qoutationRepository;
        }

        public void DeleteOrder(string orderId)
        {
            Order order = orderRepository.GetSingleByCondition(x => x.Id == orderId);
            if (order != null)
            {
                order.Active = false;
            }
        }

        public PagingResultDto<Order> GetAllByPaging(OrderFilterInput filterInput)
        {
            SetDefaultPageSize(filterInput);

            IQueryable<Order> qoutationQuery = orderRepository.GetMulti(x => x.Active == true).Include(x => x.Client).Include(x => x.OrderStatus);

            qoutationQuery = qoutationQuery.Filter(filterInput);

            PagingResultDto<Order> qoutationResult = qoutationQuery
                .PagedQuery(filterInput);

            return qoutationResult;
        }
    }
}
