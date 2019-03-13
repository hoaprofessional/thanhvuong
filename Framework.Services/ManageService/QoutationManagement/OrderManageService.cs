using Framework.Models.QoutationManagement;
using Framework.Repositories.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService.OrderManagement
{
    public interface IOrderManageService : IManageServiceBase<Order>
    {
    }
    public class OrderManageService : ManageServiceBase<Order>, IOrderManageService
    {
        public OrderManageService(IOrderRepository repository) 
            : base(repository)
        {
        }
    }
}
