using Framework.DTOs.QoutationManagementDto.BaseQoutationDto;
using Framework.Models.QoutationManagement;
using Framework.Repositories.QoutationManagement;
using Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.QoutationManagementService.QoutationListService
{
    public interface IBaseQoutationService
    {
        /// <summary>
        /// Lấy danh sách Qoutation dựa vào điều kiện filter
        /// </summary>
        /// <param name="creationUser">Người tạo</param>
        /// <param name="fromDate">Từ ngày</param>
        /// <param name="toDate">Tới ngày</param>
        /// <param name="productId">Mã sản phẩm</param>
        /// <param name="clientName">Tên khách hàng</param>
        /// <param name="QoutationStatusId">Mã trạng thái báo giá</param>
        /// <returns></returns>
        IQueryable<QoutationDto> GetQoutations(string creationUser,
            DateTime? fromDate,
            DateTime? toDate,
            String productId,
            String clientName,
            String QoutationStatusId,
            string sortingFieldName,
            string sortingAction,
            int page,
            int numberItemsPerPage,
              string currentStaffId,
               string[] permissions);


        /// <summary>
        /// Không hiển thị trên giao diện
        /// Lấy số lượng tất cả các dòng thỏa mãn điều kiện filter
        /// </summary>
        /// <param name="creationUser">Người tạo</param>
        /// <param name="fromDate">Từ ngày</param>
        /// <param name="toDate">Tới ngày</param>
        /// <param name="productId">Sản phẩm</param>
        /// <param name="clientName">Tên khách hàng</param>
        /// <param name="QoutationStatusId">Trạng thái báo giá</param>
        /// <returns></returns>
        int GetNumberOfActiveRow(string creationUser,
            DateTime? fromDate,
            DateTime? toDate,
            String productId,
            String clientName,
            String QoutationStatusId,
              string currentStaffId,
               string[] permissions);

        int GetNumberOfPages(int numberOfActiveRows, int numberOfRowsPerPage);
        List<CreateByFilterDto> GetCreateByFilters();
        List<ProductFilterDto> GetProductsFilters();
        List<ClientFilterDto> GetClientFilterDtos();
        List<QoutationStatusFilterDto> GetQoutationStatusFilterDtos();
        List<int> GetNumbersShowPage(int numberOfActiveRows);
    }
    public abstract class BaseQoutationService : IBaseQoutationService
    {
        static int NUMBER_OF_ROWS_PER_PAGE = 10;
        static int NUMBER_OF_ITEM_ROWS_MAX = 50;
        readonly IQoutationRepository QoutationRepository;
        readonly IClientRepository clientRepository;
        readonly IStaffRepository staffRepository;
        readonly IQoutationStatusRepository QoutationStatusRepository;
        readonly IQoutationDetailRepository QoutationDetailRepository;
        readonly IProductRepository productRepository;

        public BaseQoutationService(IQoutationRepository QoutationRepository,
            IClientRepository clientRepository,
            IStaffRepository staffRepository,
            IQoutationStatusRepository QoutationStatusRepository,
            IQoutationDetailRepository QoutationDetailRepository,
            IProductRepository productRepository)
        {
            this.QoutationRepository = QoutationRepository;
            this.clientRepository = clientRepository;
            this.staffRepository = staffRepository;
            this.QoutationStatusRepository = QoutationStatusRepository;
            this.QoutationDetailRepository = QoutationDetailRepository;
            this.productRepository = productRepository;
        }

        protected IQueryable<QoutationDto> GetQueryQoutation(
            string creationUser,
            DateTime? fromDate,
            DateTime? toDate,
            String productName,
            String clientName,
            String QoutationStatusId,
            string currentStaffId,
            string[] permissions)
        {
            var qoutations = QoutationRepository.GetMulti(x => x.Active == true);
            var clients = clientRepository.GetMulti(x => x.Active == true);
            var staffs = staffRepository.GetMulti(x => x.Active == true);
            var QoutationStatuss = QoutationStatusRepository.GetMulti(x => x.Active == true);
            var query = qoutations
            .LeftJoin(clients,
                        (Qoutation => Qoutation.ClientId),
                        (client => client.Id),
                        ExpressionHelper.JoinSelectResulExpression<Qoutation, Client, QoutationDto>())
             .LeftJoin(staffs,
                        (Qoutation => Qoutation.QoutationQouteStaffId),
                        (staff => staff.Id),
                        ExpressionHelper.JoinSelectResulExpression<QoutationDto, Staff>())
            .LeftJoin(QoutationStatuss,
                        (Qoutation => Qoutation.QoutationQoutationStatusId),
                        (QoutationStatus => QoutationStatus.Id),
                        ExpressionHelper.JoinSelectResulExpression<QoutationDto, QoutationStatus>());

            if (!String.IsNullOrEmpty(creationUser))
            {
                query = query.Where(x => x.StaffName.Contains(creationUser));
            }

            //lọc theo từ ngày
            if (fromDate != null)
            {
                query = query.Where(x => x.QoutationCreationTime.Value > fromDate.Value);
            }
            //lọc theo tới ngày
            if (toDate != null)
            {
                query = query.Where(x => x.QoutationCreationTime.Value < toDate.Value);
            }
            //lọc theo tên sản phẩm
            if (!String.IsNullOrEmpty(productName))
            {
                var productIds = productRepository.GetMulti(x => x.Active == true
                && x.Name.ToLower().Contains(productName.ToLower())).Select(x => x.Id).ToList().ToArray();
                var finderId = "," + String.Join(",", productIds) + ",";
                var QoutationDetails = QoutationDetailRepository.GetMulti(x => x.Active == true && finderId.Contains("," + x.ProductId + ","));
                query = query.Join(QoutationDetails,
                        (Qoutation => Qoutation.QoutationId),
                        (QoutationDetail => QoutationDetail.QoutationId),
                        ExpressionHelper.JoinSelectResulExpression<QoutationDto, QoutationDetail>());
            }
            //lọc theo tên khách hàng
            if (!String.IsNullOrEmpty(clientName))
            {
                query = query.Where(x => x.ClientName.Contains(clientName));
            }
            //lọc theo trạng thái báo giá
            if (!String.IsNullOrEmpty(QoutationStatusId))
            {
                query = query.Where(x => x.QoutationQoutationStatusId == QoutationStatusId);
            }
            InitConditionQuery(ref query, currentStaffId, permissions);
            return query;
        }

        protected abstract void InitConditionQuery(ref IQueryable<QoutationDto> query,
            string currentStaffId,
            string[] permissions);

        public virtual IQueryable<QoutationDto> GetQoutations(string creationUser,
            DateTime? fromDate,
            DateTime? toDate,
            String productId,
            String clientName,
            String QoutationStatusId,
            string sortingFieldName,
            string sortingAction,
            int page,
            int numberItemsPerPage,
            string currentStaffId,
            string[] permissions)
        {
            var query = GetQueryQoutation(creationUser,
                fromDate,
                toDate,
                productId,
                clientName,
                QoutationStatusId,
                currentStaffId,
                permissions);

            if (numberItemsPerPage == 0)
                numberItemsPerPage = NUMBER_OF_ROWS_PER_PAGE;

            // sorting
            if (sortingAction == "asc")
            {
                query = query.OrderBy(sortingFieldName);
            }
            else if (sortingAction == "desc")
            {
                query = query.OrderBy(sortingFieldName, false);
            }
            else
            {
                query = query.OrderByDescending(x => x.QoutationModifiedTime);
            }
            var result = query.Skip(page * numberItemsPerPage).Take(numberItemsPerPage);
            return result;
        }

        public int GetNumberOfActiveRow(string creationUser,
            DateTime? fromDate,
            DateTime? toDate,
            string productId,
            string clientName,
            string QoutationStatusId,
              string currentStaffId,
               string[] permissions)
        {
            var query = GetQueryQoutation(creationUser,
                fromDate,
                toDate, 
                productId, 
                clientName,
                QoutationStatusId,
                currentStaffId,
                permissions);
            return query.Count();
        }

        public int GetNumberOfPages(int numberOfActiveRows, int numberOfRowsPerPage)
        {
            if (numberOfRowsPerPage == 0)
                return 0;
            if (numberOfActiveRows % numberOfRowsPerPage == 0)
                return numberOfActiveRows / numberOfRowsPerPage;
            return numberOfActiveRows / numberOfRowsPerPage + 1;
        }

        public List<CreateByFilterDto> GetCreateByFilters()
        {
            return staffRepository
                .GetMultiBySelectClassType<CreateByFilterDto>(x => x.Active == true).ToList();
        }

        public List<ProductFilterDto> GetProductsFilters()
        {
            return productRepository
                .GetMultiBySelectClassType<ProductFilterDto>(x => x.Active == true).ToList();
        }

        public List<ClientFilterDto> GetClientFilterDtos()
        {
            return clientRepository
                .GetMultiBySelectClassType<ClientFilterDto>(x => x.Active == true).ToList();
        }

        public List<QoutationStatusFilterDto> GetQoutationStatusFilterDtos()
        {
            return QoutationStatusRepository
                .GetMultiBySelectClassType<QoutationStatusFilterDto>(x => x.Active == true).ToList();
        }

        public List<int> GetNumbersShowPage(int numberOfActiveRows)
        {
            var numberOfRows = new List<int>();
            for (int i = NUMBER_OF_ROWS_PER_PAGE; i < NUMBER_OF_ITEM_ROWS_MAX; i += NUMBER_OF_ROWS_PER_PAGE)
            {
                if (i > numberOfActiveRows)
                {
                    numberOfRows.Add(numberOfActiveRows);
                    return numberOfRows;
                }
                else
                {
                    numberOfRows.Add(i);
                }
            }
            return numberOfRows;
        }

    }
}
