using Framework.DTOs.QoutationManagementDto.CreateQoutationDto;
using Framework.Models.Configuration;
using Framework.Models.QoutationManagement;
using Framework.Repositories.Configuration;
using Framework.Repositories.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.QoutationManagementService.CreateQoutation
{
    public interface ICreateQoutationIndexService
    {
        List<ClientDto> GetClients();
        List<ProductDto> GetProducts();
        string GetProductIdByName(string productName);
        Client GetClientByNameAddressPhoneNumber(String name, string address, string phoneNumber);
        Product GetProductById(string productId);
    }
    public class CreateQoutationIndexService : ICreateQoutationIndexService
    {
        IClientRepository clientRepository;
        ICommonConfigurationRepository commonConfigurationRepository;
        IProductRepository productRepository;

        public CreateQoutationIndexService(IClientRepository clientRepository,
            IProductRepository productRepository,
            ICommonConfigurationRepository commonConfigurationRepository)
        {
            this.clientRepository = clientRepository;
            this.commonConfigurationRepository = commonConfigurationRepository;
            this.productRepository = productRepository;
        }

        public Client GetClientByNameAddressPhoneNumber(string name, string address, string phoneNumber)
        {
            return clientRepository.GetSingleByCondition(x => x.Name == name && x.Address == address && x.PhoneNumber == phoneNumber);
        }

        public List<ClientDto> GetClients()
        {
            return clientRepository.GetMultiBySelectClassType<ClientDto>(x => x.Active == true).ToList();
        }

        public Product GetProductById(string productId)
        {
            return this.productRepository.GetSingleByCondition(x => x.Id == productId);
        }

        public string GetProductIdByName(string productName)
        {
            var product = productRepository.GetSingleByCondition(x => x.Name.Contains(productName));
            if(product==null)
            {
                return null;
            }
            return product.Id;
        }

        public List<ProductDto> GetProducts()
        {
            return productRepository.GetMultiBySelectClassType<ProductDto>(x => x.Active == true).ToList();
        }
    }
}
