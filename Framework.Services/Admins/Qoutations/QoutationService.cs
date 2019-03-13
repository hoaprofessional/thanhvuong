namespace WebCore.Services.Share.Admins.Qoutations
{
    using AutoMapper;
    using Framework.Models.QoutationManagement;
    using Framework.Repositories.QoutationManagement;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using WebCore.Services.Impl.Commons;
    using WebCore.Services.Share.Admins.Qoutations.Dto;
    using WebCore.Utils.CollectionHelper;
    using WebCore.Utils.FilterHelper;
    using WebCore.Utils.ModelHelper;

    public interface IQoutationService
    {
        PagingResultDto<Qoutation> GetAllByPaging(QoutationFilterInput filterInput);
        void Delete(int qoutationId);
    }

    public class QoutationService : BaseWebService, IQoutationService
    {
        private readonly IQoutationRepository qoutationRepository;
        private readonly IMapper mapper;



        public QoutationService(IQoutationRepository qoutationRepository, IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.qoutationRepository = qoutationRepository;
        }

        public void Delete(int qoutationId)
        {
            Qoutation qoutation = qoutationRepository.GetSingleByCondition(x => x.Id == qoutationId);
            if (qoutation != null)
            {
                qoutation.Active = false;
            }
        }

        public PagingResultDto<Qoutation> GetAllByPaging(QoutationFilterInput filterInput)
        {
            SetDefaultPageSize(filterInput);

            IQueryable<Qoutation> qoutationQuery = qoutationRepository.GetMulti(x => x.Active == true).Include(x => x.Client).Include(x => x.QoutationStatus);

            qoutationQuery = qoutationQuery.Filter(filterInput);

            PagingResultDto<Qoutation> qoutationResult = qoutationQuery
                .PagedQuery(filterInput);

            return qoutationResult;
        }
    }
}
