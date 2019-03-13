using Framework.Models.QoutationManagement;
using Framework.Utils.Anotations.DtoAnotation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.QoutationManagementDto.BaseQoutationDto
{
    public class QoutationDto :
        IRef<Qoutation>,
        IRef<Client>,
        IRef<Staff>,
        IRef<QoutationStatus>
    {
        public int Id { get; set; }
        public bool CanEdit { get; set; }
        public int QoutationId { get; set; }
        //Mã người báo giá
        public String QoutationQouteStaffId { get; set; }
        //Người tạo
        public string StaffName { get; set; }
        //Mã khách hàng
        public string QoutationClientId { get; set; }
        //Khách hàng
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        //Ngày tạo
        public DateTime? QoutationCreationTime { get; set; }
        public DateTime? QoutationModifiedTime { get; set; }
        //số thư báo giá
        public String QoutationLetterOfQuotationNumber { get; set; }
        public String QoutationQoutationStatusId { get; set; }
        //trạng thái báo giá
        public String QoutationStatusName { get; set; }
        //tổng tiền
        public double QoutationTotalPriceBuy { get; set; }
        public double QoutationTotalPriceSell { get; set; }
        public decimal QoutationEstimatedInstallationTime { get; set; }
        //Ngày dự kiến bàn giao
        [NotForMap]
        public DateTime DateEstimatedHandOver
        {
            get
            {
                return QoutationCreationTime.Value.AddDays((int)QoutationEstimatedInstallationTime);
            }
        }
    }
}
