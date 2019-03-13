using AutoMapper;
using Framework.Models.QoutationManagement;
using Framework.Models.UserManagement;
using System.Collections.Generic;

namespace WebCore.Services.Share
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region AUTO MAPPER ADMIN

            CreateMap<ApplicationUser, Admins.Users.Dto.UserDto>();
            CreateMap<ApplicationUser, WebCore.Services.Share.Admins.Users.Dto.UserInfoInput>();
            CreateMap<WebCore.Services.Share.Admins.Users.Dto.UserInfoInput, ApplicationUser>();
            CreateMap<ApplicationUser, WebCore.Services.Share.Admins.Users.Dto.UserResetPasswordInput>();
            CreateMap<WebCore.Services.Share.Admins.Users.Dto.UserResetPasswordInput, ApplicationUser>();

            //CreateMap<WebCoreRole, Admins.Roles.Dto.RoleDto>();
            //CreateMap<WebCoreRole, WebCore.Services.Share.Admins.Roles.Dto.RoleInput>();
            //CreateMap<WebCore.Services.Share.Admins.Roles.Dto.RoleInput, WebCoreRole>();

            CreateMap<QoutationDetail, Framework.Services.Admins.QoutationDetails.Dto.QoutationDetailInput>();
            CreateMap<Framework.Services.Admins.QoutationDetails.Dto.QoutationDetailInput, QoutationDetail>();

            CreateMap<OrderDetail, Framework.Services.Admins.OrderDetails.Dto.OrderDetailInput>();
            CreateMap<Framework.Services.Admins.OrderDetails.Dto.OrderDetailInput, OrderDetail>();

            #endregion
        }
    }
}
