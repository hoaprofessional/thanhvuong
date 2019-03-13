using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.QoutationManagement.OrderDetail
{
    public class NoteOrderInput
    {
        [Required]
        public string OrderId { get; set; }
        public string Note { get; set; }
    }
}
