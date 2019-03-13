using Microsoft.AspNetCore.Http;
using System;
using WebCore.Utils.ModelHelper;
using WebCore.Utils.Config;
using Framework.Models;

namespace WebCore.Services.Impl.Commons
{
    public class BaseWebService
    {
        protected readonly IServiceProvider serviceProvider;
        public BaseWebService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public string GetCurrentUserLogin()
        {
            IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)serviceProvider.GetService(typeof(IHttpContextAccessor));
            return httpContextAccessor.HttpContext.User?.Identity?.Name;
        }
        protected void SetDefaultPageSize(IPagingFilterDto pagingFilterDto)
        {
            if (pagingFilterDto.PageSize <= 0)
            {
                pagingFilterDto.PageSize = 10;
            }
        }

        protected void SetAuditForInsert(IAuditable model)
        {
            model.CreationUserName = GetCurrentUserLogin();
            model.CreationTime = DateTime.Now;
            model.ModifiedUserName = GetCurrentUserLogin();
            model.ModifiedTime = DateTime.Now;
            model.Active = true;
        }

        protected void SetAuditForUpdate(IAuditable model)
        {
            model.ModifiedUserName = GetCurrentUserLogin();
            model.ModifiedTime = DateTime.Now;
        }

        protected void SetAuditForDelete(IAuditable model)
        {
            model.ModifiedUserName = GetCurrentUserLogin();
            model.ModifiedTime = DateTime.Now;
            model.Active = false;
        }
    }
}
