using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Services.ManageService.QoutationManagement;
using Framework.Services.ManageService.TaskManagement;
using Framework.Services.Utils;
using Microsoft.AspNetCore.Mvc;

namespace WebFramework.Controllers
{
    public class TestDataController : Controller
    {

        public TestDataController(IClientManageService clientManageService)
        {
            this.clientManageService = clientManageService;
        }

        IClientManageService clientManageService;
        public string TestQoutationData()
        {
            try
            {

                return "success";
            }
            catch
            {
                throw;
            }
        }
    }
}