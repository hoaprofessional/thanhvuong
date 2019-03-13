using System;
using System.ComponentModel.DataAnnotations;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.Users.Dto
{
    /// <summary>
    /// Model ApplicationUser
    /// </summary>
    public class UserInfoInput : UpdateTokenModel<string>
    {
        [Required]
        public string Name { get; set; }
        public string Contract { get; set; }
        public string Carrer { get; set; }
    }
}
