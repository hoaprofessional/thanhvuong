using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.QoutationManagement.OrderDetail
{
    public class ConfirmOrderInput
    {
        [Required]
        public string OrderId { get; set; }
        public string ApproveType { get; set; }
    }
}
