using Framework.DTOs.Management.Products;
using Framework.Models.QoutationManagement;
using Framework.Repositories.QoutationManagement;
using Framework.Utils;
using PagedList.Core;
using System.Linq;

namespace Framework.Services.ManageService.QoutationManagement
{
    public interface IProductManageService : IManageServiceBase<Product>
    {
        IPagedList<ProductDto> GetAllProducts(int pageIndex, ProductFilterModel filterModel);
    }
    public class ProductManageService : ManageServiceBase<Product>, IProductManageService
    {
        public ProductManageService(IProductRepository repository)
            : base(repository)
        {
        }

        public IPagedList<ProductDto> GetAllProducts(int pageIndex, ProductFilterModel filterModel)
        {
            var query = repository.GetMultiBySelectClassType<ProductDto>(
                x => (string.IsNullOrEmpty(filterModel.ProductName) ||
                x.Name.ToLower().Contains(filterModel.ProductName.ToLower())) &&
                (string.IsNullOrEmpty(filterModel.ProductId) ||
                x.Id.ToLower().Contains(filterModel.ProductId.ToLower())));

            switch(filterModel.ViewMode)
            {
                case Constant.ViewMode.Active:
                    query = query.Where(x => x.Active == true);
                    break;
                case Constant.ViewMode.Deleted:
                    query = query.Where(x => x.Active != true);
                    break;
            }
            query = query.OrderBy(x => x.ModifiedTime);
            return query.Paged(pageIndex);
        }
    }
}
