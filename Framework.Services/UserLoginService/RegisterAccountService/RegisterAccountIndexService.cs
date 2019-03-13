using Framework.DTOs.UserLoginDto.RegisterAccountDto;
using Framework.Models.UserManagement;
using Framework.Repositories.UserManagement;
using Framework.Utils;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.UserLoginService.RegisterAccountService
{
    public interface IRegisterAccountIndexService
    {
        List<UserObjectDto> GetUserObjectDtos();
        UserObject GetUserObjectById(string id);
        ApplicationUser GetUserByUserName(string userName);
    }
    class RegisterAccountIndexService : IRegisterAccountIndexService
    {
        IUserObjectRepository userObjectRepository;
        IApplicationUserRepository applicationUserRepository;

        public RegisterAccountIndexService(IUserObjectRepository userObjectRepository,
            IApplicationUserRepository applicationUserRepository)
        {
            this.userObjectRepository = userObjectRepository;
            this.applicationUserRepository = applicationUserRepository;
        }

        public ApplicationUser GetUserByUserName(string userName)
        {
            return applicationUserRepository.GetSingleByCondition(x => x.UserName == userName);
        }

        public UserObject GetUserObjectById(string id)
        {
            return userObjectRepository.GetSingleById(id);
        }

        public List<UserObjectDto> GetUserObjectDtos()
        {
            return userObjectRepository.GetAll().Select<UserObject, UserObjectDto>().ToList();
        }
    }
}
