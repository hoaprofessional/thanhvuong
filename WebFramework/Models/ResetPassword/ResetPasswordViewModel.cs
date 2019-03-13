using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Models.Shared;

namespace WebFramework.Models.ResetPassword
{
    public class ResetPasswordViewModel : LayoutViewModel
    {
        [Required(ErrorMessage = "Mật khẩu bắt buộc nhập")]
        [StringLength(100, ErrorMessage = "Độ dài tối thiểu là 6", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }
}
