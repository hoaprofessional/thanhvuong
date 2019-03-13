using Framework.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Controllers.UserLogin;
using WebFramework.Models.Shared;

namespace WebFramework.Models.UserLoginViewModel.HomeViewModel
{
    public class HomeIndexViewModel : LayoutViewModel, IRef<HomeController>
    {
        public String ReturnUrl { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập bắt buộc nhập")]
        [Display(Name = "Tên đăng nhập")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu bắt buộc nhập")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Duy trì đăng nhập")]
        public bool RememberMe { get; set; }
    }
}
