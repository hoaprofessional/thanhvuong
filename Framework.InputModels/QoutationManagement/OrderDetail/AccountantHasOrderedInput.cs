using Framework.DTOs;
using Framework.Models.QoutationManagement;
using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.QoutationManagement.OrderDetail
{
    public class AccountantHasOrderedInput : IRef<Order>
    {
        [Required]
        public string OrderId { get; set; }
    }
}
