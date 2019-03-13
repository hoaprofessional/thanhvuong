using Framework.DTOs.Management.Products;
using Framework.Models.QoutationManagement;
using Framework.Repositories.Infrastructor;
using Framework.Services.ManageService.QoutationManagement;
using Framework.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Areas.Management.Models;
using WebFramework.Infrastructor;

namespace WebFramework.Areas.Management.Controllers
{
    [Area("Management")]
    [ClaimRequirement(MyClaimType.Permission, PermissionValue.RegisterUser)]
    public class ProductController : AdminBaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductManageService productManageService;

        public ProductController(IProductManageService productManageService,IServiceProvider serviceProvider, IUnitOfWork unitOfWork) :
            base(serviceProvider)
        {
            this.productManageService = productManageService;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index(int page = 1)
        {
            ProductFilterModel filterModel = GetFilterInSession();
            ProductViewModel productViewModel = new ProductViewModel
            {
                Products = productManageService.GetAllProducts(page, filterModel),
                FilterModel = filterModel
            };
            InitAdminBaseViewModel(productViewModel);
            return View(productViewModel);
        }

        public ActionResult SaveFilter(ProductFilterModel filterModel)
        {
            SetFilterToSession(filterModel);
            return Redirect("/Management/Product");
        }

        public JsonResult DeleteProduct(string id)
        {
            Product product = productManageService.GetById(id);
            if (product == null)
            {
                return Json("fail");
            }
            productManageService.Delete(productManageService.GetById(id));
            unitOfWork.Commit();
            return Json("success");
        }

        public JsonResult RestoreProduct(string id)
        {
            Product product = productManageService.GetById(id);
            if (product == null)
            {
                return Json("fail");
            }
            if (product.Active == true)
            {
                return Json("fail");
            }
            product.Active = true;
            productManageService.Update(productManageService.GetById(id));
            unitOfWork.Commit();
            return Json("success");
        }

        public ProductFilterModel GetFilterInSession()
        {
            try
            {
                string filter = HttpContext.Session.GetString(Constant.SessionName.ProductSession);
                if (filter == null)
                {
                    ProductFilterModel filterModel = new ProductFilterModel
                    {
                        ViewMode = Constant.ViewMode.Active
                    };

                    return filterModel;
                }
                else
                {
                    return JsonConvert.DeserializeObject<ProductFilterModel>(filter);
                }
            }
            catch
            {
                return new ProductFilterModel();
            }
        }
        public void SetFilterToSession(ProductFilterModel filter)
        {
            string json = JsonConvert.SerializeObject(filter);
            HttpContext.Session.SetString(Constant.SessionName.ProductSession, json);
        }

        [HttpPost]
        public async Task<JsonResult> SubmitInput(ProductInput input)
        {
            if (!ModelState.IsValid)
            {
                return Json("fail");
            }
            input.Id = input.Id.Trim();
            if (input.Files != null && input.Files.Count > 0)
            {
                IFormFile file = input.Files.First();
                input.Images = Guid.NewGuid() + ".png";
                string path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/upload/",
                        input.Images);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                input.Images = "/upload/" + input.Images;
            }
            else
            {
                input.Images = null;
            }


            Product product = productManageService.GetById(input.Id);
            if (product == null)
            {
                product = new Product();
                product.CopyFrom(input);
                productManageService.Add(product);
            }
            else
            {
                product.CopyFrom(input);
                productManageService.Update(product);
            }
            unitOfWork.Commit();
            return Json("success");
        }
    }
}