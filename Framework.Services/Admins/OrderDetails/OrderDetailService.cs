namespace WebCore.Services.Share.Admins.OrderDetails
{
    using AutoMapper;
    using Framework.Models.QoutationManagement;
    using Framework.Repositories.QoutationManagement;
    using Framework.Services.Admins.OrderDetails.Dto;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using WebCore.Services.Impl.Commons;
    using WebCore.Services.Share.Admins.OrderDetails.Dto;
    using WebCore.Utils.CollectionHelper;
    using WebCore.Utils.FilterHelper;
    using WebCore.Utils.ModelHelper;

    public interface IOrderDetailService
    {
        PagingResultDto<OrderDetail> GetAllByPaging(OrderDetailFilterInput filterInput);
        bool UpdateOrderDetail(OrderDetailInput orderDetailInput);
        OrderDetailInput GetInputById(string id);
        OrderDetail GetById(string id);
    }

    public class OrderDetailService : BaseWebService, IOrderDetailService
    {
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;



        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository, IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider)
        {
            this.orderDetailRepository = orderDetailRepository;
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public PagingResultDto<OrderDetail> GetAllByPaging(OrderDetailFilterInput filterInput)
        {
            SetDefaultPageSize(filterInput);

            IQueryable<OrderDetail> orderDetailQuery = orderDetailRepository.GetAll().Include(x => x.Product).Include(x => x.Order);

            orderDetailQuery = orderDetailQuery.Where(x => x.OrderId == filterInput.OrderId).Filter(filterInput);

            PagingResultDto<OrderDetail> orderDetailResult = orderDetailQuery
                .PagedQuery(filterInput);

            return orderDetailResult;
        }

        public OrderDetail GetById(string id)
        {
            return orderDetailRepository.GetSingleById(id);
        }

        public OrderDetailInput GetInputById(string id)
        {
            OrderDetail entity = orderDetailRepository.GetSingleById(id);

            OrderDetailInput updateInput = new OrderDetailInput();

            if (entity == null)
            {
                return null;
            }

            updateInput = mapper.Map<OrderDetailInput>(entity);

            return updateInput;
        }

        public bool UpdateOrderDetail(OrderDetailInput orderDetailInput)
        {
            OrderDetail entity = orderDetailRepository.GetSingleById(orderDetailInput.Id);

            if (entity == null)
            {
                return false;
            }

            mapper.Map(orderDetailInput, entity);

            SetAuditForUpdate(entity);
            orderDetailRepository.Update(entity);
            Order order = orderRepository.GetSingleByCondition(x => x.Id == entity.OrderId);
            order.TotalPrice = (double)orderDetailRepository.GetAll()
                .Where(x => x.OrderId == entity.OrderId && x.Active.Value).ToList()
                .Sum(x => x.UnitPrice * x.ProductQuantity + x.UnitPrice * (decimal)x.VAT) - (double)order.Promotion;
            orderRepository.Update(order);
            return true;
        }
    }
}
