using Framework.DTOs.QoutationManagementDto.BaseQoutationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFramework.Models.QoutationManagementViewModels.AllQoutationViewModels
{
    public class AllQoutationListPartialViewModel
    {
        public List<QoutationDto> Qoutations { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public string ColumnSortingName { get; set; }
        public string SortingAction { get; set; }
    }
}
