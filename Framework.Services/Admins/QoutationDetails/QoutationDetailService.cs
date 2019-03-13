namespace WebCore.Services.Share.Admins.QoutationDetails
{
    using AutoMapper;
    using Framework.Models.QoutationManagement;
    using Framework.Repositories.QoutationManagement;
    using Framework.Services.Admins.QoutationDetails.Dto;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using WebCore.Services.Impl.Commons;
    using WebCore.Services.Share.Admins.QoutationDetails.Dto;
    using WebCore.Utils.CollectionHelper;
    using WebCore.Utils.FilterHelper;
    using WebCore.Utils.ModelHelper;

    public interface IQoutationDetailService
    {
        PagingResultDto<QoutationDetail> GetAllByPaging(QoutationDetailFilterInput filterInput);
        bool UpdateQoutationDetail(QoutationDetailInput qoutationDetailInput);
        QoutationDetailInput GetInputById(string id);
        QoutationDetail GetById(string id);
    }

    public class QoutationDetailService : BaseWebService, IQoutationDetailService
    {
        private readonly IQoutationDetailRepository qoutationDetailRepository;
        private readonly IQoutationRepository qoutationRepository;
        private readonly IMapper mapper;



        public QoutationDetailService(IQoutationDetailRepository qoutationDetailRepository, IQoutationRepository qoutationRepository, IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider)
        {
            this.qoutationDetailRepository = qoutationDetailRepository;
            this.qoutationRepository = qoutationRepository;
            this.mapper = mapper;
        }

        public PagingResultDto<QoutationDetail> GetAllByPaging(QoutationDetailFilterInput filterInput)
        {
            SetDefaultPageSize(filterInput);

            IQueryable<QoutationDetail> qoutationDetailQuery = qoutationDetailRepository.GetAll().Include(x => x.Product).Include(x => x.Qoutation);

            qoutationDetailQuery = qoutationDetailQuery.Where(x => x.QoutationId == filterInput.QoutationId).Filter(filterInput);

            PagingResultDto<QoutationDetail> qoutationDetailResult = qoutationDetailQuery
                .PagedQuery(filterInput);

            return qoutationDetailResult;
        }

        public QoutationDetail GetById(string id)
        {
            return qoutationDetailRepository.GetSingleById(id);
        }

        public QoutationDetailInput GetInputById(string id)
        {
            QoutationDetail entity = qoutationDetailRepository.GetSingleById(id);

            QoutationDetailInput updateInput = new QoutationDetailInput();

            if (entity == null)
            {
                return null;
            }

            updateInput = mapper.Map<QoutationDetailInput>(entity);

            return updateInput;
        }

        public bool UpdateQoutationDetail(QoutationDetailInput qoutationDetailInput)
        {
            QoutationDetail entity = qoutationDetailRepository.GetSingleById(qoutationDetailInput.Id);

            if (entity == null)
            {
                return false;
            }

            mapper.Map(qoutationDetailInput, entity);

            SetAuditForUpdate(entity);
            qoutationDetailRepository.Update(entity);
            Qoutation qoutation = qoutationRepository.GetSingleByCondition(x => x.Id == entity.QoutationId);
            qoutation.TotalPriceBuy = (double)qoutationDetailRepository.GetAll()
                .Where(x => x.QoutationId == entity.QoutationId && x.Active.Value).ToList()
                .Sum(x => x.UnitPriceBuy * x.ProductQuantity + x.UnitPriceBuy * (decimal)x.VATBuy);
            qoutation.TotalPriceSell = (double)qoutationDetailRepository.GetAll()
                .Where(x => x.QoutationId == entity.QoutationId && x.Active.Value).ToList()
                .Sum(x => x.UnitPriceSell * x.ProductQuantity + x.UnitPriceSell * (decimal)x.VATBuy);
            qoutationRepository.Update(qoutation);
            return true;
        }
    }
}
