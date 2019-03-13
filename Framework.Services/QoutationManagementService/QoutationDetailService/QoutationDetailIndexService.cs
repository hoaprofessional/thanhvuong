using Framework.DTOs.QoutationManagementDto.QoutationDetailDto.QoutationDetailGetProductDto;
using Framework.Models.Configuration;
using Framework.Models.QoutationManagement;
using Framework.Repositories.Configuration;
using Framework.Repositories.QoutationManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.QoutationManagementService.QoutationDetailService
{
    public interface IQoutationDetailService
    {
        Qoutation GetQoutationById(int id);
        CommonConfiguration GetCommonConfiguration();
        List<QoutationDetail> GetQoutationDetailsByQoutationId(int QoutationId);
        ProductResultDto GetProductById(string productId);
    }
    public class QoutationDetailIndexService : IQoutationDetailService
    {
        IQoutationRepository qoutationRespository;
        IQoutationDetailRepository qoutationDetailRepository;
        ICommonConfigurationRepository commonConfigurationRepository;
        IProductRepository productRepository;

        public QoutationDetailIndexService(IQoutationRepository QoutationRespository,
            IQoutationDetailRepository QoutationDetailRepository,
            IProductRepository productRepository,
            ICommonConfigurationRepository commonConfigurationRepository)
        {
            this.qoutationRespository = QoutationRespository;
            this.commonConfigurationRepository = commonConfigurationRepository;
            this.qoutationDetailRepository = QoutationDetailRepository;
            this.productRepository = productRepository;
        }

        public CommonConfiguration GetCommonConfiguration()
        {
            return commonConfigurationRepository.GetAll().SingleOrDefault();
        }

        public ProductResultDto GetProductById(string productId)
        {
            return productRepository.GetMultiBySelectClassType<ProductResultDto>
                (x => x.Id == productId && x.Active == true).SingleOrDefault();
        }

        public Qoutation GetQoutationById(int id)
        {
            var query = qoutationRespository.GetMulti(x => x.Id == id && x.Active == true).
                Include(x => x.Client).
                Include(x => x.ConfirmStaff).
                Include(x => x.Manager).
                Include(x => x.QouteStaff).
                Include(x => x.QoutationStatus).
                Include(x => x.SalesAdmin);
            var qoutation = query.SingleOrDefault();
            if (qoutation == null)
                return null;
            qoutation.QoutationDetails = qoutationDetailRepository.GetMulti(x => x.Active == true && x.QoutationId == id).Include(x => x.Product).ToList();
            return qoutation;
        }

        public List<QoutationDetail> GetQoutationDetailsByQoutationId(int QoutationId)
        {
            return qoutationDetailRepository.GetMulti(x => x.Active == true
            && x.QoutationId == QoutationId).ToList();
        }
    }
}
