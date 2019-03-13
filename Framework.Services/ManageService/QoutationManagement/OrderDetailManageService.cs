using Framework.Models.QoutationManagement;
using Framework.Repositories.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService.OrderManagement
{
    public interface IOrderDetailManageService : IManageServiceBase<OrderDetail>
    {

    }
    public class OrderDetailManageService : ManageServiceBase<OrderDetail>, IOrderDetailManageService
    {
        public OrderDetailManageService(IOrderDetailRepository repository) 
            : base(repository)
        {
        }
    }
}
