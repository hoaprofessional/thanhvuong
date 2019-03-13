using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Models.Configuration;
using Framework.Models.QoutationManagement;
using Framework.Repositories.Configuration;
using Framework.Repositories.QoutationManagement;
using Microsoft.EntityFrameworkCore;

namespace Framework.Services.QoutationManagementService.OrderDetailService
{
    public interface IOrderDetailIndexService
    {
        Order GetOrderById(string id);
        OrderDetail GetOrderDetailByProductId(string orderId, string productId);
        CommonConfiguration GetCommonConfiguration();
        List<OrderDetail> GetOrderDetailsByOrderId(string id);
    }
    public class OrderDetailIndexService : IOrderDetailIndexService
    {
        readonly IOrderRepository orderRepository;
        readonly IQoutationRepository qoutationRepository;
        readonly IOrderDetailRepository orderDetailRepository;
        readonly ICommonConfigurationRepository commonConfigurationRepository;

        public OrderDetailIndexService(IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IQoutationRepository qoutationRepository,
            ICommonConfigurationRepository commonConfigurationRepository)
        {
            this.orderRepository = orderRepository;
            this.commonConfigurationRepository = commonConfigurationRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.qoutationRepository = qoutationRepository;
        }

        public CommonConfiguration GetCommonConfiguration()
        {
            return commonConfigurationRepository.GetAll().SingleOrDefault();
        }

        public Order GetOrderById(string id)
        {
            var order = orderRepository.GetMulti(x => x.Id == id && x.Active == true).
                Include(x => x.OrderStatus).
                Include(x=>x.Client).
                Include(x=>x.CreationStaff).
                SingleOrDefault();
            if (order == null)
                return null;
            //order.Qoutation = qoutationRepository.
            //    GetMulti(x => x.Id == order.QoutationId && x.Active == true).
            //    Include(x => x.Client).
            //    Include(x => x.ConfirmStaff).
            //    Include(x => x.Manager).
            //    Include(x => x.QouteStaff).
            //    Include(x => x.QoutationStatus).
            //    Include(x => x.SalesAdmin).SingleOrDefault();
            
            if(order.Client==null)
            {
                order.Client =
                    qoutationRepository
                    .GetMulti(x => x.Id == order.QoutationId)
                    .Include(x => x.Client).SingleOrDefault().Client;
            }
            order.OrderDetails = orderDetailRepository.GetMulti(x => x.Active == true
            && x.OrderId == id).Include(x => x.Product).ToList();
            return order;
        }

        public OrderDetail GetOrderDetailByProductId(string orderId, string productId)
        {
            return orderDetailRepository.GetSingleByCondition(x => x.OrderId == orderId && x.ProductId == productId);
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(string id)
        {
            return orderDetailRepository.GetMulti(x => x.Active == true
            && x.OrderId == id).ToList();
        }
    }
}
