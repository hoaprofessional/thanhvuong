using Framework.DTOs;
using Framework.DTOs.UserLoginDto.RegisterAccountDto;
using Framework.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Models.Shared;

namespace WebFramework.Models.UserLoginViewModel.RegisterAccountViewModel
{
    public class RegisterAccountIndexViewModel : LayoutViewModel, IRef<ApplicationUser>
    {
        [Required(ErrorMessage ="Tên đăng nhập không được để trống")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage ="Số điện thoại không đúng định dạng")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }

        [Required(ErrorMessage = "Chứng minh nhân dân không được để trống")]
        [Display(Name = "Chứng minh nhân dân")]
        public String IdentityCardNumber { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, ErrorMessage = "Mật khẩu không được nhỏ hơn 8 ký tự.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu nhập lại không đúng.")]
        public string ConfirmPassword { get; set; }
        public String ObjectId { get; set; }
        public List<UserObjectDto> UserObjectDtos { get; set; }
    }
}
