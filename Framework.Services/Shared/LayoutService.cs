using Framework.DTOs.LayoutDto;
using Framework.Models.Configuration;
using Framework.Models.NotificationManagement;
using Framework.Models.QoutationManagement;
using Framework.Models.UserManagement;
using Framework.Repositories.Configuration;
using Framework.Repositories.NotificationManagement;
using Framework.Repositories.QoutationManagement;
using Framework.Repositories.UserManagement;
using Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.Shared
{
    public interface ILayoutService
    {
        ApplicationUser GetUserById(string userId);
        ApplicationUser GetUserByUserName(string userName);
        Staff GetStaffByUserId(string userId);
        CommonConfiguration GetCommonConfiguration();
        List<NotificationDto> GetTopNotifications(string staffId);
        int GetUnReadNotificationCount(string staffId);
        string GetPermissionNotification(string qoutationStatusId);
        List<ApplicationUser> GetUsersByClaim(string claimValue);
    }
    public class LayoutService : ILayoutService
    {
        IApplicationUserRepository applicationUserRepository;
        IStaffRepository staffRepository;
        ICommonConfigurationRepository commonConfigurationRepository;
        readonly INotificationRepository notificationRepository;
        IQoutationSendNotificationConfigRepository qoutationSendNotificationConfigRepository;
        
        public LayoutService(IApplicationUserRepository applicationUserRepository,
            IStaffRepository staffRepository,
            ICommonConfigurationRepository commonConfigurationRepository,
            IQoutationSendNotificationConfigRepository qoutationSendNotificationConfigRepository,
            INotificationRepository notificationRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.staffRepository = staffRepository;
            this.notificationRepository = notificationRepository;
            this.commonConfigurationRepository = commonConfigurationRepository;
            this.qoutationSendNotificationConfigRepository = qoutationSendNotificationConfigRepository;
        }

        public List<NotificationDto> GetTopNotifications(string staffId)
        {
            return this.notificationRepository.GetMulti(x => x.Active == true && x.StaffId == staffId)
                .OrderByDescending(x => x.CreationTime)
                .Select<Notification, NotificationDto>()
                .Take(10)
                .ToList();
        }

        public Staff GetStaffByUserId(string userId)
        {
            return this.staffRepository.GetSingleByCondition(x => x.UserId == userId);
        }

        public ApplicationUser GetUserById(string userId)
        {
            return this.applicationUserRepository.GetSingleById(userId);
        }

        public ApplicationUser GetUserByUserName(string userName)
        {
            return this.applicationUserRepository.GetSingleByCondition(x => x.UserName == userName);
        }

        public int GetUnReadNotificationCount(string staffId)
        {
            return this.notificationRepository.GetMulti(x => x.Active == true && x.IsRead == false && x.StaffId==staffId).Count();
        }

        public CommonConfiguration GetCommonConfiguration()
        {
            return commonConfigurationRepository.GetAll().SingleOrDefault();
        }

        public string GetPermissionNotification(string qoutationStatusId)
        {
            var permission = this.qoutationSendNotificationConfigRepository.GetSingleByCondition(x => x.QoutationStatus == qoutationStatusId);
            if (permission == null)
                return null;
            return permission.SendtoPermission;
        }

        public List<ApplicationUser> GetUsersByClaim(string claimValue)
        {
            var dbContext = applicationUserRepository.DbContext;
            var users = dbContext.Users;
            var roles = dbContext.Roles;
            var roleClaims = dbContext.RoleClaims;
            var userClaims = dbContext.UserClaims;
            var userRoles = dbContext.UserRoles;
            return (from user in users
                    where userClaims.Any(x => x.ClaimValue == claimValue && x.UserId == user.Id)
                    ||
                    roleClaims.Any(roleClaim => roleClaim.ClaimValue == claimValue && userRoles.Any(userRole => userRole.RoleId == roleClaim.RoleId && userRole.UserId==user.Id))
                    select user).ToList();
        }
    }
}
