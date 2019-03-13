using Framework.DTOs;
using Framework.Models.QoutationManagement;
using System.Collections.Generic;
using System.Text;

namespace Framework.InputModels.QoutationManagement.CreateOrder
{
    public class CreateOrderInput : IRef<Order>
    {
        public int? QoutationId { get; set; }
        public decimal Promotion { get; set; }
        public List<OrderProductInput> Products { get; set; }
    }
}
