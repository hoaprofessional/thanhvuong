using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.QoutationManagement.OrderDetail
{
    public class UpdateReadyToDeliverInput
    {
        [Required]
        public string OrderId { get; set; }
    }
}
