using FT_ProviderSys.Configs;
using FT_ProviderSys.DTOs;
using FT_ProviderSys.DTOs.Validators;
using FT_ProviderSys.Models;
using FT_ProviderSys.Repositories.Interfaces;
using FT_ProviderSys.Services.Interfaces;

namespace FT_ProviderSys.Services
{
    public class ProviderService : IProviderService
    {
        public static List<Provider> providerSample = new List<Provider>();
        private static int nextId = 1;
        private readonly IProviderRepository _providerRepository;
        private readonly IValidationHelper _validation;

        public ProviderService(IProviderRepository providerRepository, IValidationHelper validation)
        {
            _providerRepository = providerRepository;
            _validation = validation;
        }

        public List<Provider> GetAll() => providerSample;

        public Provider? GetById(int id)
        {
            return providerSample.FirstOrDefault(x => x.ProviderId == id);
        }

        public async Task<int> Create(ProviderCreateRequestDTO inputProvider)
        {
            await _validation.ValidateAsync<ProviderCreateRequestDTOValidator, ProviderCreateRequestDTO>(inputProvider);

            var newProvider = new Provider(
                                inputProvider.CorporateName, inputProvider.LegalEntityIdentifier, 
                                inputProvider.State, inputProvider.ContactEmail, inputProvider.ContactEmail);

            await _providerRepository.Add(newProvider);
            return newProvider.ProviderId;
        }

        public async Task<bool> Update(ProviderUpdateRequestDTO inputProvider)
        {
            await _validation.ValidateAsync<ProviderUpdateRequestDTOValidator, ProviderUpdateRequestDTO>(inputProvider);

            var existingProvider = providerSample.Find(x => x.ProviderId == inputProvider.ProviderId);
            if (existingProvider == null) return false;

            var index = providerSample.IndexOf(existingProvider);

            existingProvider.CorporateName = inputProvider.CorporateName;
            existingProvider.LegalEntityIdentifier = inputProvider.LegalEntityIdentifier;
            existingProvider.State = inputProvider.State;
            existingProvider.ContactEmail = inputProvider.ContactEmail;
            existingProvider.ContactName = inputProvider.ContactName;

            providerSample[index] = existingProvider;

            return true;
        }

        public bool Delete(int id)
        {
            var existingProvider = providerSample.Find(x => x.ProviderId == id);
            if (existingProvider == null) return false;

            providerSample.Remove(existingProvider);

            return true;
        }
    }
}
