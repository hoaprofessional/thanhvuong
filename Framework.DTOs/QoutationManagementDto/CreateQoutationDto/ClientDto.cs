using Framework.Models.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.QoutationManagementDto.CreateQoutationDto
{
    public class ClientDto : IRef<Client>
    {
        public String Name { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
    }
}
