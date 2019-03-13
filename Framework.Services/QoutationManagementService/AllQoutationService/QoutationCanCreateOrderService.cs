using Framework.DTOs.QoutationManagementDto.BaseQoutationDto;
using Framework.Repositories.QoutationManagement;
using Framework.Services.QoutationManagementService.QoutationListService;
using Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.QoutationManagementService.AllQoutationService
{
    public interface IQoutationCanCreateOrderService : IBaseQoutationService
    {

    }
    public class QoutationCanCreateOrderService : BaseQoutationService,
        IQoutationCanCreateOrderService
    {
        public QoutationCanCreateOrderService(IQoutationRepository QoutationRepository, IClientRepository clientRepository, IStaffRepository staffRepository, IQoutationStatusRepository QoutationStatusRepository, IQoutationDetailRepository QoutationDetailRepository, IProductRepository productRepository) : base(QoutationRepository, clientRepository, staffRepository, QoutationStatusRepository, QoutationDetailRepository, productRepository)
        {
        }

        protected override void InitConditionQuery(ref IQueryable<QoutationDto> query,
            string currentStaffId,
            string[] permissions)
        {
            query = query.Where(x => x.QoutationQoutationStatusId == QoutationStatusIdHelper.SalesManagerApproveAccountingDepartment ||
            x.QoutationQoutationStatusId == QoutationStatusIdHelper.ClientAccepted);
        }
    }
}
