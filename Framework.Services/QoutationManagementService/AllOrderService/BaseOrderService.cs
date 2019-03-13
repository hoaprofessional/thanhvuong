using Framework.DTOs.QoutationManagementDto.BaseOrderDto;
using Framework.Models.QoutationManagement;
using Framework.Repositories.QoutationManagement;
using Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.OrderManagementService.OrderListService
{
    public interface IBaseOrderService
    {
        /// <summary>
        /// Lấy danh sách Order dựa vào điều kiện filter
        /// </summary>
        /// <param name="creationUser">Người tạo</param>
        /// <param name="fromDate">Từ ngày</param>
        /// <param name="toDate">Tới ngày</param>
        /// <param name="productId">Mã sản phẩm</param>
        /// <param name="clientName">Tên khách hàng</param>
        /// <param name="OrderStatusId">Mã trạng thái báo giá</param>
        /// <returns></returns>
        IQueryable<OrderDto> GetOrders(string creationUser,
            DateTime? fromDate,
            DateTime? toDate,
            String productId,
            String clientName,
            String OrderStatusId,
            string sortingFieldName,
            string sortingAction,
            int page,
            int numberItemsPerPage,
            string currentStaffId,
            string[] permissions);

        /// <summary>
        /// Không hiển thị trên giao diện
        /// Lấy số lượng tất cả các dòng thỏa mãn điều kiện filter
        /// </summary>
        /// <param name="creationUser">Người tạo</param>
        /// <param name="fromDate">Từ ngày</param>
        /// <param name="toDate">Tới ngày</param>
        /// <param name="productId">Sản phẩm</param>
        /// <param name="clientName">Tên khách hàng</param>
        /// <param name="OrderStatusId">Trạng thái báo giá</param>
        /// <returns></returns>
        int GetNumberOfActiveRow(string creationUser,
            DateTime? fromDate,
            DateTime? toDate,
            String productId,
            String clientName,
            String OrderStatusId,
            string currentStaffId,
            string[] permissions);

        int GetNumberOfPages(int numberOfActiveRows, int numberOfRowsPerPage);
        List<CreateByFilterDto> GetCreateByFilters();
        List<ProductFilterDto> GetProductsFilters();
        List<ClientFilterDto> GetClientFilterDtos();
        List<OrderStatusFilterDto> GetOrderStatusFilterDtos();
        List<int> GetNumbersShowPage(int numberOfActiveRows);
    }
    public abstract class BaseOrderService : IBaseOrderService
    {
        static int NUMBER_OF_ROWS_PER_PAGE = 10;
        static int NUMBER_OF_ITEM_ROWS_MAX = 50;
        readonly IOrderRepository orderRepository;
        readonly IQoutationRepository qoutationRepository;
        readonly IClientRepository clientRepository;
        readonly IStaffRepository staffRepository;
        readonly IOrderStatusRepository OrderStatusRepository;
        readonly IOrderDetailRepository OrderDetailRepository;
        readonly IProductRepository productRepository;

        public BaseOrderService(IOrderRepository orderRepository,
            IClientRepository clientRepository,
            IStaffRepository staffRepository,
            IOrderStatusRepository orderStatusRepository,
            IOrderDetailRepository orderDetailRepository,
            IQoutationRepository qoutationRepository,
            IProductRepository productRepository)
        {
            this.orderRepository = orderRepository;
            this.clientRepository = clientRepository;
            this.staffRepository = staffRepository;
            this.OrderStatusRepository = orderStatusRepository;
            this.OrderDetailRepository = orderDetailRepository;
            this.productRepository = productRepository;
            this.qoutationRepository = qoutationRepository;
        }

        protected IQueryable<OrderDto> GetQueryOrder(
            string creationUser,
            DateTime? fromDate,
            DateTime? toDate,
            String productName,
            String clientName,
            String orderStatusId,
            string currentStaffId,
            string[] permissions)
        {
            var orders = orderRepository.GetMulti(x => x.Active == true);
            //var qoutations = qoutationRepository.GetMulti(x => x.Active == true);
            var clients = clientRepository.GetMulti(x => x.Active == true);
            var staffs = staffRepository.GetMulti(x => x.Active == true);
            var orderStatuss = OrderStatusRepository.GetMulti(x => x.Active == true);
            

            var query = orders
            .LeftJoin(clients,
                        (order => order.ClientId),
                        (client => client.Id),
                        ExpressionHelper.JoinSelectResulExpression<Order, Client, OrderDto>())
             .LeftJoin(staffs,
                        (order => order.OrderCreationStaffId),
                        (staff => staff.Id),
                        ExpressionHelper.JoinSelectResulExpression<OrderDto, Staff>())
            .LeftJoin(orderStatuss,
                        (Order => Order.OrderOrderStatusId),
                        (OrderStatus => OrderStatus.Id),
                        ExpressionHelper.JoinSelectResulExpression<OrderDto, OrderStatus>());

            if (!String.IsNullOrEmpty(creationUser))
            {
                query = query.Where(x => x.StaffName.Contains(creationUser));
            }

            //lọc theo từ ngày
            if (fromDate != null)
            {
                query = query.Where(x => x.OrderCreationTime.Value > fromDate.Value);
            }
            //lọc theo tới ngày
            if (toDate != null)
            {
                query = query.Where(x => x.OrderCreationTime.Value < toDate.Value);
            }
            //lọc theo tên sản phẩm
            if (!String.IsNullOrEmpty(productName))
            {
                var productIds = productRepository.GetMulti(x => x.Active == true
                && x.Name.ToLower().Contains(productName.ToLower())).Select(x => x.Id).ToList().ToArray();
                var finderId = "," + String.Join(",", productIds) + ",";
                var OrderDetails = OrderDetailRepository.GetMulti(x => x.Active == true && finderId.Contains("," + x.ProductId + ","));
                query = query.Join(OrderDetails,
                        (order => order.OrderId),
                        (OrderDetail => OrderDetail.OrderId),
                        ExpressionHelper.JoinSelectResulExpression<OrderDto, OrderDetail>());
            }
            //lọc theo tên khách hàng
            if (!String.IsNullOrEmpty(clientName))
            {
                query = query.Where(x => x.ClientName.Contains(clientName));
            }
            //lọc theo trạng thái báo giá
            if (!String.IsNullOrEmpty(orderStatusId))
            {
                query = query.Where(x => x.OrderOrderStatusId == orderStatusId);
            }
            InitConditionQuery(ref query,currentStaffId, permissions);
            return query;
        }

        protected abstract void InitConditionQuery(ref IQueryable<OrderDto> query,
            string currentStaffId,
            string[] permissions);

        public virtual IQueryable<OrderDto> GetOrders(string creationUser,
            DateTime? fromDate,
            DateTime? toDate,
            String productId,
            String clientName,
            String OrderStatusId,
            string sortingFieldName,
            string sortingAction,
            int page,
            int numberItemsPerPage,
            string currentStaffId,
            string[] permissions)
        {
            if (numberItemsPerPage == 0)
                numberItemsPerPage = NUMBER_OF_ROWS_PER_PAGE;
            var query = GetQueryOrder(creationUser, 
                fromDate, 
                toDate, 
                productId, 
                clientName, 
                OrderStatusId,
                currentStaffId,
                permissions);

            // sorting
            if (sortingAction == "asc")
            {
                query = query.OrderBy(sortingFieldName);
            }
            else if (sortingAction == "desc")
            {
                query = query.OrderBy(sortingFieldName, false);
            }
            else
            {
                query = query.OrderByDescending(x => x.OrderModifiedTime);
            }
            var result = query.Skip(page * numberItemsPerPage).Take(numberItemsPerPage);
            return result;
        }

        public int GetNumberOfActiveRow(string creationUser, 
            DateTime? fromDate, 
            DateTime? toDate, 
            string productId, 
            string clientName, 
            string OrderStatusId,
            string currentStaffId,
            string[] permissions)
        {
            var query = GetQueryOrder(creationUser, 
                fromDate, 
                toDate, 
                productId, 
                clientName,
                OrderStatusId,
                currentStaffId,
                permissions);
            return query.Count();
        }

        public int GetNumberOfPages(int numberOfActiveRows, int numberOfRowsPerPage)
        {
            if (numberOfRowsPerPage == 0)
                return 0;
            if (numberOfActiveRows % numberOfRowsPerPage == 0)
                return numberOfActiveRows / numberOfRowsPerPage;
            return numberOfActiveRows / numberOfRowsPerPage + 1;
        }

        public List<CreateByFilterDto> GetCreateByFilters()
        {
            return staffRepository
                .GetMultiBySelectClassType<CreateByFilterDto>(x => x.Active == true).ToList();
        }

        public List<ProductFilterDto> GetProductsFilters()
        {
            return productRepository
                .GetMultiBySelectClassType<ProductFilterDto>(x => x.Active == true).ToList();
        }

        public List<ClientFilterDto> GetClientFilterDtos()
        {
            return clientRepository
                .GetMultiBySelectClassType<ClientFilterDto>(x => x.Active == true).ToList();
        }

        public List<OrderStatusFilterDto> GetOrderStatusFilterDtos()
        {
            return OrderStatusRepository
                .GetMultiBySelectClassType<OrderStatusFilterDto>(x => x.Active == true).ToList();
        }

        public List<int> GetNumbersShowPage(int numberOfActiveRows)
        {
            var numberOfRows = new List<int>();
            for (int i = NUMBER_OF_ROWS_PER_PAGE; i < NUMBER_OF_ITEM_ROWS_MAX; i += NUMBER_OF_ROWS_PER_PAGE)
            {
                if (i > numberOfActiveRows)
                {
                    numberOfRows.Add(numberOfActiveRows);
                    return numberOfRows;
                }
                else
                {
                    numberOfRows.Add(i);
                }
            }
            return numberOfRows;
        }
    }
}
