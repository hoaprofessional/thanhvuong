using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.QoutationManagement.OrderDetail
{
    public class UpdateRecommendedDeliveryInput
    {
        [Required]
        public string OrderId { get; set; }
    }
}
