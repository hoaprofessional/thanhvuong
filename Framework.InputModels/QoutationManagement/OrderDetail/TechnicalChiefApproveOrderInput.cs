using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.QoutationManagement.OrderDetail
{
    public class TechnicalChiefApproveOrderInput
    {
        [Required]
        public string OrderId { get; set; }
        [Required]
        public DateTime ExpectedDeliveryDate { get; set; }
    }
}
