using FT_ProviderSys.Configs;
using FT_ProviderSys.DTOs;
using FT_ProviderSys.DTOs.Validators;
using FT_ProviderSys.Exceptions;
using FT_ProviderSys.Models;
using FT_ProviderSys.Repositories.Interfaces;
using FT_ProviderSys.Services.Interfaces;

namespace FT_ProviderSys.Services
{
    public class ProviderService : IProviderService
    {
        //public static List<Provider> providerSample = new List<Provider>();
        //private static int nextId = 1;
        private readonly IProviderRepository _providerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IValidationHelper _validation;

        public ProviderService(
            IProviderRepository providerRepository, 
            IOrderRepository orderRepository,
            IValidationHelper validation)
        {
            _providerRepository = providerRepository;
            _orderRepository = orderRepository;
            _validation = validation;
        }

        public async Task<IEnumerable<Provider>> GetAll() => await _providerRepository.GetAll();

        public async Task<Provider?> GetById(int id)
        {
            // validating input data
            await _validation.ValidateAsync<IdRequestDTOValidator, IdRequestDTO>(new IdRequestDTO { Id = id });

            // interaction with the database 
            var result = await _providerRepository.GetById(id);

            if (result == null)
                throw new NotFoundException("There is no 'Provider' with this id.");

            return result;
        }

        public async Task<int> Create(ProviderCreateRequestDTO inputProvider)
        {
            // validating input data
            await _validation.ValidateAsync<ProviderCreateRequestDTOValidator, ProviderCreateRequestDTO>(inputProvider);

            // interaction with the database 
            var newProvider = new Provider(
                                inputProvider.CorporateName, inputProvider.LegalEntityIdentifier, 
                                inputProvider.State, inputProvider.ContactEmail, inputProvider.ContactEmail);

            await _providerRepository.Add(newProvider);
            return newProvider.ProviderId;
        }

        public async Task<bool> Update(ProviderUpdateRequestDTO inputProvider)
        {
            // validating input data
            await _validation.ValidateAsync<ProviderUpdateRequestDTOValidator, ProviderUpdateRequestDTO>(inputProvider);

            // checking data integrity
            if (!(await _providerRepository.ExistAsync(inputProvider.ProviderId)))
                throw new NotFoundException("There is no 'Provider' with this id.");

            // interaction with the database 
            var existingProvider = await _providerRepository.GetById(inputProvider.ProviderId);

            existingProvider.CorporateName = inputProvider.CorporateName;
            existingProvider.LegalEntityIdentifier = inputProvider.LegalEntityIdentifier;
            existingProvider.State = inputProvider.State;
            existingProvider.ContactEmail = inputProvider.ContactEmail;
            existingProvider.ContactName = inputProvider.ContactName;

            await _providerRepository.Update(existingProvider);

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            // validating input data
            await _validation.ValidateAsync<IdRequestDTOValidator, IdRequestDTO>(new IdRequestDTO { Id = id });

            // checking data integrity
            if (!(await _providerRepository.ExistAsync(id)))
                throw new NotFoundException("There is no 'Provider' with this id.");
            if (await _orderRepository.ExistByProvider(id))
                throw new BadRequestException("Its not possible to delete a 'Provider' that has 'Orders'.");

            // interaction with the database
            await _providerRepository.Delete(id);

            return true;
        }
    }
}
