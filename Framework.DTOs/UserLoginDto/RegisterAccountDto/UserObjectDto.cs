using Framework.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.UserLoginDto.RegisterAccountDto
{
    public class UserObjectDto : IRef<UserObject>
    {
        public String Id { get; set; }
        public String Name { get; set; }
    }
}
