using Framework.DTOs.QoutationManagementDto.BaseQoutationDto;
using Framework.Repositories.QoutationManagement;
using Framework.Services.QoutationManagementService.QoutationListService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.QoutationManagementService.AllQoutationService
{
    public interface IAllQoutationIndexService : IBaseQoutationService
    {
    }
    public class AllQoutationIndexService :
        BaseQoutationService,
        IAllQoutationIndexService
    {
        public AllQoutationIndexService(IQoutationRepository QoutationRepository, IClientRepository clientRepository, IStaffRepository staffRepository, IQoutationStatusRepository QoutationStatusRepository, IQoutationDetailRepository QoutationDetailRepository, IProductRepository productRepository) : base(QoutationRepository, clientRepository, staffRepository, QoutationStatusRepository, QoutationDetailRepository, productRepository)
        {
        }

        protected override void InitConditionQuery(ref IQueryable<QoutationDto> query,
            string currentStaffId,
            string[] permissions)
        {
        }
    }
}
