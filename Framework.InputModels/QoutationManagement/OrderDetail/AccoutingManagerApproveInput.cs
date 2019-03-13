using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.QoutationManagement.OrderDetail
{
    public class AccoutingManagerApproveInput
    {
        [Required]
        public string OrderId { get; set; }
        public string ApproveType { get; set; }
    }
}
