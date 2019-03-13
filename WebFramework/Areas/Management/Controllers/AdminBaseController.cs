using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebCore.Areas.Admin.Models;
using WebCore.Utils.ModelHelper;

public class AdminBaseController : Controller
{
    protected IServiceProvider serviceProvider;

    public AdminBaseController(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    protected void InitAdminBaseViewModel(AdminBaseViewModel adminBaseViewModel)
    {
        var permissions = GetAllPermissions();
        adminBaseViewModel.CurrentLanguage = CultureInfo.CurrentCulture.Name;
        //List<LanguageDto> languages = languageAdminService.GetAllLanguages();
        //SelectList languagesSelectList = new SelectList(languages, nameof(LanguageDto.LangCode), nameof(LanguageDto.LangName));
        //ViewBag.Languages = languagesSelectList;
        adminBaseViewModel.CurrentUserName = GetCurrentUserLogin();
        string currentLink = Request.Path.ToString();
    }

    public string GetCurrentUserLogin()
    {
        IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)serviceProvider.GetService(typeof(IHttpContextAccessor));
        return httpContextAccessor.HttpContext.User?.Identity?.Name;
    }

    protected string[] GetAllPermissions()
    {
        if (HttpContext.User == null)
        {
            return new string[0];
        }
        return HttpContext.User.Claims.Where(x => x.Type == "Permission").Select(x => x.Value).ToArray();
    }

    protected TFilter GetFilterInSession<TFilter>(string sessionName) where TFilter : new()
    {
        try
        {
            string filter = HttpContext.Session.GetString(sessionName);
            if (filter == null)
            {
                TFilter filterModel = new TFilter();
                return filterModel;
            }
            else
            {
                return JsonConvert.DeserializeObject<TFilter>(filter);
            }
        }
        catch
        {
            return new TFilter();
        }
    }

    protected IEnumerable<ModelStateErrorModel> GetModelErrors()
    {
        return ModelState
            .Where(x => x.Value.Errors.Any())
            .Select(x => new ModelStateErrorModel()
            {
                PropertyName = x.Key,
                ErrorMessages = x.Value.Errors.Select(e => e.ErrorMessage)
            });
    }

    protected void SetFilterToSession(string sessionName, object filterObject)
    {
        string jsonString = JsonConvert.SerializeObject(filterObject);
        HttpContext.Session.SetString(sessionName, jsonString);
    }

    protected bool HasPermission(string permission)
    {
        return HttpContext.User.Claims.Any(x => x.Value == permission);
    }
    
}
