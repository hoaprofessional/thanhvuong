using Framework.DTOs.QoutationManagementDto.BaseQoutationDto;
using System.Collections.Generic;

namespace WebFramework.Models.QoutationManagementViewModels.AllQoutationViewModels
{
    public class QoutationCanCreatePartialViewModel
    {
        public List<QoutationDto> Qoutations { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public string ColumnSortingName { get; set; }
        public string SortingAction { get; set; }
    }
}
