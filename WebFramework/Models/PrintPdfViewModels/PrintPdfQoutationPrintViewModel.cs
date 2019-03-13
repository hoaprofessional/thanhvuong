using Framework.DTOs.PrintPdfDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Models.Shared;

namespace WebFramework.Models.PrintPdfViewModels
{
    public class PrintPdfQoutationPrintViewModel : LayoutViewModel
    {
        public QoutationDto QoutationDto { get; set; }
    }
}
