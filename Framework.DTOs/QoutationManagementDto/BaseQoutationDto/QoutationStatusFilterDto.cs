using Framework.Models.QoutationManagement;

namespace Framework.DTOs.QoutationManagementDto.BaseQoutationDto
{
    public class QoutationStatusFilterDto : IRef<QoutationStatus>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
