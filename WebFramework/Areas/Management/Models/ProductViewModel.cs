using Framework.DTOs.Management.Products;
using Framework.Utils;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Areas.Admin.Models;

namespace WebFramework.Areas.Management.Models
{
    public class ProductViewModel : AdminBaseViewModel
    {
        public IPagedList<ProductDto> Products { get; set; }
        public ProductFilterModel FilterModel { get; set; }
    }
}
