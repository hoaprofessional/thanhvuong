using Framework.DTOs.QoutationManagementDto.BaseQoutationDto;
using Framework.Repositories.QoutationManagement;
using Framework.Services.QoutationManagementService.QoutationListService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.QoutationManagementService.AllQoutationService
{
    public interface IAllQoutationIndexService : IBaseQoutationService
    {
        List<string> GetAllDiscount();
        List<double> GetAllPrice();
    }
    public class AllQoutationIndexService :
        BaseQoutationService,
        IAllQoutationIndexService
    {
        public AllQoutationIndexService(IQoutationRepository qoutationRepository, IClientRepository clientRepository, IStaffRepository staffRepository, IQoutationStatusRepository QoutationStatusRepository, IQoutationDetailRepository qoutationDetailRepository, IProductRepository productRepository) 
            : base(qoutationRepository, clientRepository, staffRepository, QoutationStatusRepository, qoutationDetailRepository, productRepository)
        {
        }

        public List<string> GetAllDiscount()
        {
            return qoutationDetailRepository.DbSet.Select(x => x.Discount).ToList();
        }

        public List<double> GetAllPrice()
        {
            return qoutationDetailRepository.DbSet.Select(x => x.TotalPriceBuy).ToList();
        }

        protected override void InitConditionQuery(ref IQueryable<QoutationDto> query,
            string currentStaffId,
            string[] permissions)
        {
        }
    }
}
