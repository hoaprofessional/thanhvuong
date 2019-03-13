using Framework.Models.QoutationManagement;
using Framework.Repositories.QoutationManagement;
using Framework.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService.QoutationManagement
{
    public interface IQoutationEventManageService : IManageServiceBase<QoutationEvent>
    {
        /// <summary>
        /// Trạng thái báo giá : vừa được tạo
        /// </summary>
        /// <param name="salesId">Mã nhân viên sale tạo báo giá</param>
        /// <param name="QoutationId">Mã báo giá được tạo</param>
        /// <returns>Quá trình xử lý hóa đơn khi được thêm</returns>
        QoutationEvent AlreadyCreated(String salesId, int QoutationId);

        /// <summary>
        /// Trạng thái báo giá: Trưởng phòng sale đã phê duyệt báo giá từ nhân viên sale
        /// </summary>
        /// <param name="salesManagerId">Mã trưởng phòng sale duyệt báo giá</param>
        /// <param name="QoutationId">Mã báo giá được phê duyệt</param>
        /// <returns>Quá trình xử lý hóa đơn khi được thêm</returns>
        QoutationEvent SalesManagerApproveSalesStaff(String salesManagerId, int QoutationId);

        /// <summary>
        /// Trạng thái báo giá: Nhân viên kế toán đã điền giá mua
        /// </summary>
        /// <param name="accountantId">Mã nhân viên kế toán điền giá</param>
        /// <param name="QoutationId">Mã báo giá được điền giá</param>
        /// <returns>Quá trình xử lý hóa đơn khi được thêm</returns>
        QoutationEvent AccountantFilledPriceBuy(String accountantId, int QoutationId);

        /// <summary>
        /// Trạng thái báo giá: Nhân viên kế toán đã điền giá bán và đang đợi phê duyệt
        /// </summary>
        /// <param name="accountantId">Mã nhân viên kế toán điền giá</param>
        /// <param name="QoutationId">Mã báo giá được điền giá</param>
        /// <returns>Quá trình xử lý hóa đơn khi được thêm</returns>
        QoutationEvent AccountantFilledPriceSell(String accountantId, int QoutationId);
        QoutationEvent AccountantRejectPriceBuy(String accountantId, int QoutationId);

        /// <summary>
        /// Trạng thái báo giá : Trưởng phòng kế toán đã phê duyệt giá của báo giá
        /// </summary>
        /// <param name="accountingManagerId">Mã trưởng phòng kế toán phê duyệt giá</param>
        /// <param name="QoutationId">Mã báo giá được phê duyệt</param>
        /// <returns>Quá trình xử lý hóa đơn khi được thêm</returns>
        QoutationEvent AccountingManagerApprovedPrice(String accountingManagerId, int QoutationId);

        /// <summary>
        /// Trạng thái báo giá : Trưởng phòng kế toán không phê duyệt giá của báo giá
        /// </summary>
        /// <param name="accountingManagerId">Mã trưởng phòng kế toán</param>
        /// <param name="QoutationId">Mã báo giá không được phê duyệt</param>
        /// <returns>Quá trình xử lý hóa đơn khi được thêm</returns>
        QoutationEvent AccountingManagerDontApprovedPrice(String accountingManagerId, int QoutationId);

        /// <summary>
        /// Trạng thái báo giá : Trưởng phòng sale đã phê duyệt báo giá từ phòng kế toán
        /// </summary>
        /// <param name="saleManagerId">Mã trưởng phòng sale phê duyệt</param>
        /// <param name="QoutationId">Mã báo giá được phê duyệt</param>
        /// <returns>Quá trình xử lý hóa đơn khi được thêm</returns>
        QoutationEvent SalesManagerApproveAccountingDepartment(String saleManagerId, int QoutationId);

        /// <summary>
        /// Trạng thái báo giá : Trưởng phòng sale không phê duyệt báo giá từ phòng kế toán
        /// </summary>
        /// <param name="saleManagerId">Mã trưởng phòng sale</param>
        /// <param name="QoutationId">Mã báo giá không được phê duyệt</param>
        /// <returns>Quá trình xử lý hóa đơn khi được thêm</returns>
        QoutationEvent SalesManagerDontApproveAccountingDepartment(String saleManagerId, int QoutationId);

        /// <summary>
        /// Trạng thái báo giá : Đang báo giá
        /// </summary>
        /// <param name="salesId">Mã nhân viên sale báo giá</param>
        /// <param name="QoutationId">Mã báo giá báo giá</param>
        /// <returns>Quá trình xử lý hóa đơn khi được thêm</returns>
        QoutationEvent Quoting(String salesId, int QoutationId);

        /// <summary>
        /// Trạng thái báo giá : Khách hàng đã đồng ý báo giá
        /// </summary>
        /// <param name="salesId">Mã nhân viên sale</param>
        /// <param name="QoutationId">Mã báo giá được đồng ý</param>
        /// <returns>Quá trình xử lý hóa đơn khi được thêm</returns>
        QoutationEvent ClientAccepted(String salesId, int QoutationId);

        /// <summary>
        /// Trạng thái báo giá : Khách hàng không đồng ý báo giá
        /// </summary>
        /// <param name="salesId">Mã nhân viên sale</param>
        /// <param name="QoutationId">Mã báo giá không được đồng ý</param>
        /// <returns>Quá trình xử lý hóa đơn khi được thêm</returns>
        QoutationEvent ClientRejected(String salesId, int QoutationId);

        /// <summary>
        /// Trạng thái báo giá : Đã hủy
        /// Nếu salesManagerId=null thì báo giá bị hủy do hệ thống
        /// </summary>
        /// <param name="salesManagerId">Mã trưởng phòng hủy báo giá. Nếu bằng null là do hệ thống tự hủy</param>
        /// <param name="QoutationId">Mã báo giá bị hủy</param>
        /// <returns>Quá trình xử lý hóa đơn khi được thêm</returns>
        QoutationEvent Terminated(String salesManagerId, int QoutationId);

        String GetStaffIdCreateStatus(string qoutationStatusId, int qoutationId);
    }
    public class QoutationEventManageService : ManageServiceBase<QoutationEvent>, IQoutationEventManageService
    {
        IStaffRepository staffRepository;
        IQoutationStatusRepository QoutationStatusRepository;
        public QoutationEventManageService(IQoutationEventRepository repository,
            IStaffRepository staffRepository,
            IQoutationStatusRepository QoutationStatusRepository)
            : base(repository)
        {
            this.staffRepository = staffRepository;
            this.QoutationStatusRepository = QoutationStatusRepository;
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
        public QoutationEvent AccountantFilledPriceBuy(string accountantId, int qoutationId)
        {
            QoutationEvent QoutationEvent = new QoutationEvent()
            {
                QoutationId = qoutationId,
                StaffId = accountantId,
                QoutationStatusId = QoutationStatusIdHelper.AccountantFilledPriceBuy,
                Note = String.Format("Báo giá {0} được NV {1} điền giá mua.", qoutationId, GetStaffName(accountantId))
            };
            return Add(QoutationEvent);
        }

        public QoutationEvent AccountantFilledPriceSell(string accountantId, int qoutationId)
        {
            QoutationEvent QoutationEvent = new QoutationEvent()
            {
                QoutationId = qoutationId,
                StaffId = accountantId,
                QoutationStatusId = QoutationStatusIdHelper.AccountantFilledPriceSell,
                Note = String.Format("Báo giá {0} được NV {1} điền giá bán.", qoutationId, GetStaffName(accountantId))
            };
            return Add(QoutationEvent);
        }

        public QoutationEvent AccountantRejectPriceBuy(string accountantId, int qoutationId)
        {
            QoutationEvent QoutationEvent = new QoutationEvent()
            {
                QoutationId = qoutationId,
                StaffId = accountantId,
                QoutationStatusId = QoutationStatusIdHelper.AccountantFilledPriceSell,
                Note = String.Format("Báo giá {0} được NV {1} yêu cầu điền lại giá.", qoutationId, GetStaffName(accountantId))
            };
            return Add(QoutationEvent);
        }

        public QoutationEvent AccountingManagerApprovedPrice(string accountingManagerId, int qoutationId)
        {
            QoutationEvent QoutationEvent = new QoutationEvent()
            {
                QoutationId = qoutationId,
                StaffId = accountingManagerId,
                QoutationStatusId = QoutationStatusIdHelper.AccountingManagerApprovedPrice,
                Note = String.Format("Báo giá {0} được NV {1} phê duyệt giá.", qoutationId, GetStaffName(accountingManagerId))
            };
            return Add(QoutationEvent);
        }

        public QoutationEvent AccountingManagerDontApprovedPrice(string accountingManagerId, int qoutationId)
        {
            QoutationEvent QoutationEvent = new QoutationEvent()
            {
                QoutationId = qoutationId,
                StaffId = accountingManagerId,
                QoutationStatusId = QoutationStatusIdHelper.AccountingManagerRejectApprovedPrice,
                Note = String.Format("Báo giá {0} không được NV {1} phê duyệt giá.", qoutationId, GetStaffName(accountingManagerId))
            };
            return Add(QoutationEvent);
        }

        public QoutationEvent AlreadyCreated(string salesId, int qoutationId)
        {
            QoutationEvent QoutationEvent = new QoutationEvent()
            {
                QoutationId = qoutationId,
                StaffId = salesId,
                QoutationStatusId = QoutationStatusIdHelper.AlreadyCreated,
                Note = String.Format("Báo giá {0} được tạo bởi NV sale {1}.", qoutationId, GetStaffName(salesId))
            };
            return Add(QoutationEvent);
        }

        public QoutationEvent ClientAccepted(string salesId, int qoutationId)
        {
            QoutationEvent QoutationEvent = new QoutationEvent()
            {
                QoutationId = qoutationId,
                StaffId = salesId,
                QoutationStatusId = QoutationStatusIdHelper.ClientAccepted,
                Note = String.Format("Khách hàng đồng ý báo giá {0}", qoutationId)
            };
            return Add(QoutationEvent);
        }

        public QoutationEvent ClientRejected(string salesId, int qoutationId)
        {
            QoutationEvent QoutationEvent = new QoutationEvent()
            {
                QoutationId = qoutationId,
                StaffId = salesId,
                QoutationStatusId = QoutationStatusIdHelper.ClientRejected,
                Note = String.Format("Khách hàng không đồng ý báo giá {0}", qoutationId)
            };
            return Add(QoutationEvent);
        }

        public QoutationEvent Quoting(string salesId, int qoutationId)
        {
            QoutationEvent QoutationEvent = new QoutationEvent()
            {
                QoutationId = qoutationId,
                StaffId = salesId,
                QoutationStatusId = QoutationStatusIdHelper.Quoting,
                Note = String.Format("Báo giá {0}", qoutationId)
            };
            return Add(QoutationEvent);
        }

        public QoutationEvent SalesManagerApproveAccountingDepartment(string saleManagerId, int qoutationId)
        {
            QoutationEvent qoutationEvent = new QoutationEvent()
            {
                QoutationId = qoutationId,
                StaffId = saleManagerId,
                QoutationStatusId = QoutationStatusIdHelper.SalesManagerApproveAccountingDepartment,
                Note = String.Format("NV {0} phê duyệt báo giá {1} từ phòng kế toán", GetStaffName(saleManagerId), qoutationId)
            };
            return Add(qoutationEvent);
        }

        public QoutationEvent SalesManagerApproveSalesStaff(string salesManagerId, int qoutationId)
        {
            QoutationEvent QoutationEvent = new QoutationEvent()
            {
                QoutationId = qoutationId,
                StaffId = salesManagerId,
                QoutationStatusId = QoutationStatusIdHelper.SalesManagerApproveSalesStaff,
                Note = String.Format("NV {0} phê duyệt báo giá {1} vừa được tạo", GetStaffName(salesManagerId), qoutationId)
            };
            return Add(QoutationEvent);
        }

        public QoutationEvent SalesManagerDontApproveAccountingDepartment(string saleManagerId, int qoutationId)
        {
            QoutationEvent QoutationEvent = new QoutationEvent()
            {
                QoutationId = qoutationId,
                StaffId = saleManagerId,
                QoutationStatusId = QoutationStatusIdHelper.Quoting,
                Note = String.Format("NV {0} không phê duyệt báo giá {1} từ phòng kế toán", GetStaffName(saleManagerId), qoutationId)
            };
            return Add(QoutationEvent);
        }

        public QoutationEvent Terminated(string salesManagerId, int qoutationId)
        {
            String note = "";
            if (salesManagerId == null)
            {
                note = String.Format("Báo giá {0} bị hủy do quá 15 ngày chưa xử lý", qoutationId);
            }
            else
            {
                String.Format("Báo giá {0} bị hủy bởi NV {1}", GetStaffName(salesManagerId), qoutationId);
            }

            QoutationEvent QoutationEvent = new QoutationEvent()
            {
                QoutationId = qoutationId,
                StaffId = salesManagerId,
                QoutationStatusId = QoutationStatusIdHelper.Terminated,
                Note = note
            };
            return Add(QoutationEvent);
        }

        public string GetStaffIdCreateStatus(string qoutationStatusId, int qoutationId)
        {
            return repository.GetSingleByCondition(x => x.QoutationStatusId == qoutationStatusId
                                        && x.QoutationId == qoutationId).StaffId;
        }
    }
}
