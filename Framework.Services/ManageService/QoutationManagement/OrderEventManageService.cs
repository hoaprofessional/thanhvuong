using Framework.Models.QoutationManagement;
using Framework.Repositories.QoutationManagement;
using Framework.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService.QoutationManagement
{
    public interface IOrderEventManageService : IManageServiceBase<OrderEvent>
    {
        OrderEvent AlreadyCreated(String staffId, String OrderId);
        OrderEvent SalesManagerApprove(String staffId, String OrderId);
        OrderEvent ManagerConfirm(string staffId, string orderId);
        OrderEvent ManagerReject(string staffId, string orderId);
        OrderEvent SalesManagerReject(String staffId, String OrderId);
        OrderEvent AccountantHasOrder(String staffId, String OrderId);
        OrderEvent AccoutantChangeReturnGoodDate(String staffId, String OrderId);
        OrderEvent AccountingManagerApproveOrder(String staffId, String OrderId);
        OrderEvent AccountingManagerRejectOrder(String staffId, String OrderId);
        OrderEvent GoodOnWay(String staffId, String OrderId);
        OrderEvent ReadyToDeliver(String staffId, String OrderId);
        OrderEvent RecomendDeliver(String staffId, String OrderId);
        OrderEvent ChiefTechnicalApprove(String staffId, String OrderId);
        OrderEvent ChiefTechnicalDeliver(String staffId, String OrderId);
        OrderEvent AccountantReceiveMoney(String staffId, String OrderId);
        OrderEvent ClientDept(String staffId, String OrderId);
        String GetStaffIdCreateStatus(string orderStatusId, string orderId);
    }
    public class OrderEventManageService : ManageServiceBase<OrderEvent>, IOrderEventManageService
    {
        IStaffRepository staffRepository;
        public OrderEventManageService(IOrderEventRepository repository,
            IStaffRepository staffRepository)
            : base(repository)
        {
            this.staffRepository = staffRepository;
        }
        /// <summary>
        /// Lấy tên nhân viên từ mã nhân viên
        /// </summary>
        /// <param name="staffId">Mã nhân viên</param>
        /// <returns>Quá trình xử lý hóa đơn khi được thêm</returns>
        private String GetStaffName(string staffId)
        {
            if (staffId == null)
            {
                return "Hệ thống";
            }
            return staffRepository.GetSingleById(staffId).Name;
        }
        public OrderEvent AlreadyCreated(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.AlreadyCreated,
                Note = String.Format("Đơn hàng {0} được tạo bởi {1}.", orderId, GetStaffName(staffId))
            };
            return Add(orderEvent);
        }

        public OrderEvent SalesManagerApprove(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.SalesManagerApprove,
                Note = String.Format("Đơn hàng {0} vừa tạo được duyệt bởi {1}.", orderId, GetStaffName(staffId))
            };
            return Add(orderEvent);
        }

        public OrderEvent ManagerConfirm(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.RejectOrder,
                Note = String.Format("Đơn hàng {0} được xác nhận bởi {1}.", orderId, GetStaffName(staffId))
            };
            return Add(orderEvent);
        }

        public OrderEvent ManagerReject(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.ConfirmOrder,
                Note = String.Format("Đơn hàng {0} không được xác nhận bởi {1}.", orderId, GetStaffName(staffId))
            };
            return Add(orderEvent);
        }

        public OrderEvent SalesManagerReject(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.SalesManagerReject,
                Note = String.Format("Đơn hàng {0} vừa tạo không được duyệt duyệt bởi {1}.", orderId, GetStaffName(staffId))
            };
            return Add(orderEvent);
        }

        public OrderEvent AccountantHasOrder(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.AccountantHasOrdered,
                Note = String.Format("Nhân viên {0} đã đặt hàng cho đơn hàng {1}.", GetStaffName(staffId), orderId)
            };
            return Add(orderEvent);
        }

        public OrderEvent AccoutantChangeReturnGoodDate(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.AccountantHasOrdered,
                Note = String.Format("Nhân viên {0} thay đổi ngày dự kiến giao hàng của đơn hàng {1}.", GetStaffName(staffId), orderId)
            };
            return Add(orderEvent);
        }

        public OrderEvent GoodOnWay(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.GoodOnWay,
                Note = String.Format("Hàng của đơn hàng {0} đang trên đường về. Nhân viên cập nhật: {1}.", orderId, GetStaffName(staffId))
            };
            return Add(orderEvent);
        }

        public OrderEvent ReadyToDeliver(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.ReadyToDeliver,
                Note = String.Format("Đơn hàng {0}. Hàng sẵn sàng giao. Nhân viên cập nhật: {0}.", orderId, GetStaffName(staffId))
            };
            return Add(orderEvent);
        }

        public OrderEvent ChiefTechnicalApprove(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.ChiefTechnicalApprove,
                Note = String.Format("Trưởng phòng kỹ thuật {0} duyệt đơn hàng {1}.", GetStaffName(staffId), orderId)
            };
            return Add(orderEvent);
        }

        public OrderEvent ChiefTechnicalDeliver(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.ChiefTechnicalDeliver,
                Note = String.Format("Trưởng phòng kỹ thuật {0} đã giao hàng đơn hàng {1}.", GetStaffName(staffId), orderId)
            };
            return Add(orderEvent);
        }

        public OrderEvent AccountantReceiveMoney(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.AccountantReceiveMoney,
                Note = String.Format("Nhân viên {0} đã thu tiền đơn hàng {1}.", GetStaffName(staffId), orderId)
            };
            return Add(orderEvent);
        }

        public OrderEvent ClientDept(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.ClientDept,
                Note = String.Format("Đơn hàng {0}. Khách hàng nợ, Nhân viên cập nhật: {1}.", orderId, GetStaffName(staffId))
            };
            return Add(orderEvent);
        }

        public OrderEvent AccountingManagerApproveOrder(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.AccountingManagerApprove,
                Note = String.Format("Trưởng phòng kế toán {0} đã duyệt đặt hàng {1}.", GetStaffName(staffId), orderId)
            };
            return Add(orderEvent);
        }

        public OrderEvent AccountingManagerRejectOrder(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.AccountingManagerReject,
                Note = String.Format("Trưởng phòng kế toán {0} đã từ chối đơn đặt hàng {1}.", GetStaffName(staffId), orderId)
            };
            return Add(orderEvent);
        }

        public OrderEvent RecomendDeliver(string staffId, string orderId)
        {
            OrderEvent orderEvent = new OrderEvent()
            {
                OrderId = orderId,
                StaffId = staffId,
                OrderStatusId = OrderStatusIdHelper.RecommendedDelivery,
                Note = "Yêu cầu đặt hàng " + orderId
            };
            return Add(orderEvent);
        }

        public string GetStaffIdCreateStatus(string orderStatusId, string orderId)
        {
            return repository.GetSingleByCondition(x => x.OrderStatusId == orderStatusId && x.OrderId == orderId).StaffId;
        }
    }
}
