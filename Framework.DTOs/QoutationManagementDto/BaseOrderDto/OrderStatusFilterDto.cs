using Framework.Models.QoutationManagement;

namespace Framework.DTOs.QoutationManagementDto.BaseOrderDto
{
    public class OrderStatusFilterDto : IRef<OrderStatus>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
