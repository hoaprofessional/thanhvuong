using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.InputModels.QoutationManagement.QoutationDetail
{
    public class SalesManagerApprovePriceInput
    {
        [Required]
        public int QoutationId { get; set; }
        public String ApproveType { get; set; }
    }
}
