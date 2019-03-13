using Framework.Models.QoutationManagement;

namespace Framework.DTOs.QoutationManagementDto.BaseOrderDto
{
    public class ProductFilterDto : IRef<Product>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
