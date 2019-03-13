using Framework.DTOs.QoutationManagementDto.CreateOrderDto;
using Framework.Models.QoutationManagement;
using Framework.Repositories.Infrastructor;
using Framework.Repositories.QoutationManagement;
using Framework.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.QoutationManagementService.CreateOrderService
{
    public interface ICreateOrderIndexService
    {
        /// <summary>
        /// Sửa status thành khách hàng đồng ý
        /// </summary>
        /// <param name="qoutationId"></param>
        /// <returns></returns>
        bool FixQoutationStatus(int qoutationId);
        Qoutation GetQoutationById(int qoutationId);
        List<QoutationDetail> GetQoutationDetails(int qoutationId);

        List<ClientDto> GetClients();
        List<ProductDto> GetProducts();
        string GetProductIdByName(string productName);
        Client GetClientByNameAddressPhoneNumber(String name, string address, string phoneNumber);
        Product GetProductById(string productId);
    }
    public class CreateOrderIndexService : ICreateOrderIndexService
    {
        IOrderRepository orderRepository;
        IQoutationRepository qoutationRepository;
        IQoutationDetailRepository qoutationDetailRepository;
        IClientRepository clientRepository;
        IProductRepository productRepository;
        IUnitOfWork unitOfWork;
        public CreateOrderIndexService(IOrderRepository orderRepository,
            IQoutationRepository qoutationRepository,
            IClientRepository clientRepository,
            IProductRepository productRepository,
            IQoutationDetailRepository qoutationDetailRepository,
            IUnitOfWork unitOfWork)
        {
            this.orderRepository = orderRepository;
            this.qoutationRepository = qoutationRepository;
            this.qoutationDetailRepository = qoutationDetailRepository;
            this.clientRepository = clientRepository;
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }
        

        public bool FixQoutationStatus(int qoutationId)
        {
            var qoutation = qoutationRepository.GetMulti(x => x.Id == qoutationId)
                .SingleOrDefault();
            if(qoutation == null)
            {
                return false;
            }

            if(qoutation.QoutationStatusId != QoutationStatusIdHelper.SalesManagerApproveAccountingDepartment)
            {
                return false;
            }

            qoutation.QoutationStatusId = QoutationStatusIdHelper.ClientAccepted;
            qoutationRepository.Update(qoutation);
            unitOfWork.Commit();
            return true;
        }

        public Qoutation GetQoutationById(int qoutationId)
        {
            var query = qoutationRepository.GetMulti(x => x.Id == qoutationId && x.Active == true).
                Include(x => x.Client).
                Include(x => x.ConfirmStaff).
                Include(x => x.Manager).
                Include(x => x.QouteStaff).
                Include(x => x.QoutationStatus).
                Include(x => x.SalesAdmin);
            var Qoutation = query.SingleOrDefault();
            if (Qoutation == null)
                return null;
            Qoutation.QoutationDetails = qoutationDetailRepository.GetMulti(x => x.Active == true && x.QoutationId == qoutationId).Include(x => x.Product).ToList();
            return Qoutation;
        }

        public List<QoutationDetail> GetQoutationDetails(int qoutationId)
        {
            return qoutationDetailRepository.GetMulti(x => x.Active == true && x.QoutationId == qoutationId).ToList();
        }


        public Client GetClientByNameAddressPhoneNumber(string name, string address, string phoneNumber)
        {
            return clientRepository.GetSingleByCondition(x => x.Name == name && x.Address == address && x.PhoneNumber == phoneNumber);
        }

        public List<ClientDto> GetClients()
        {
            return clientRepository.GetMultiBySelectClassType<ClientDto>(x => x.Active == true).ToList();
        }


        public string GetProductIdByName(string productName)
        {
            var product = productRepository.GetSingleByCondition(x => x.Name.Contains(productName));
            if (product == null)
            {
                return null;
            }
            return product.Id;
        }

        public List<ProductDto> GetProducts()
        {
            return productRepository.GetMultiBySelectClassType<ProductDto>(x => x.Active == true).ToList();
        }

        public Product GetProductById(string productId)
        {
            return this.productRepository.GetSingleByCondition(x => x.Id == productId);
        }
    }
}
