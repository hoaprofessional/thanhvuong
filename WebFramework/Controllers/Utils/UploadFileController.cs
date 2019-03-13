using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebFramework.Controllers.Utils
{
    public class UploadFileController : Controller
    {

        [HttpPost]
        public async Task<JsonResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Json("fail");
            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        "upload/" + Guid.NewGuid().ToString() + ".png");
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Json(path);
        }

    }
}