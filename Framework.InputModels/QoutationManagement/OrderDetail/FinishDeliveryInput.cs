using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.QoutationManagement.OrderDetail
{
    public class FinishDeliveryInput
    {
        [Required]
        public string OrderId { get; set; }
        public decimal PaidPrice { get; set; }
    }
}
