using Framework.Models.QoutationManagement;

namespace Framework.DTOs.QoutationManagementDto.BaseQoutationDto
{
    public class ClientFilterDto : IRef<Client>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
