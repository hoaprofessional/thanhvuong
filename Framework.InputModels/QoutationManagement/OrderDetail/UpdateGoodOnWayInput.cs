using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.QoutationManagement.OrderDetail
{
    public class UpdateGoodOnWayInput
    {
        [Required]
        public string OrderId { get; set; }
    }
}
