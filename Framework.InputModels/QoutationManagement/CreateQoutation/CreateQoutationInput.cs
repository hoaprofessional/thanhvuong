using Framework.DTOs;
using Framework.Models.QoutationManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.InputModels.QoutationManagement.CreateQoutation
{
    public class CreateQoutationInput : IRef<Client>, IRef<Qoutation>
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public String Address { get; set; }
        [Required]
        public String PhoneNumber { get; set; }
        public String LetterOfQuotationNumber { get; set; }
        public String DeliveryPlace { get; set; }
        public decimal EstimatedInstallationTime { get; set; }
        [Required]
        public QoutationProductInput[] Products { get; set; }
    }
}
