using FT_ProductSys.DTOs.Validators;
using FT_ProviderSys.Configs;
using FT_ProviderSys.DTOs;
using FT_ProviderSys.DTOs.Validators;
using FT_ProviderSys.Exceptions;
using FT_ProviderSys.Models;
using FT_ProviderSys.Repositories.Interfaces;
using FT_ProviderSys.Services.Interfaces;

namespace FT_ProviderSys.Services
{
    public class ProductService : IProductService
    {
        //public static List<Product> productSample = new List<Product>();
        //private static int nextId = 1;
        private readonly IProductRepository _productRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IValidationHelper _validation;

        public ProductService(
            IProductRepository productRepository,
            IOrderProductRepository orderProductRepository,
            IValidationHelper validation)
        {
            _productRepository = productRepository;
            _orderProductRepository = orderProductRepository;
            _validation = validation;
        }

        public async Task<IEnumerable<Product>> GetAll() => await _productRepository.GetAll();

        public async Task<Product?> GetById(int id)
        {
            // validating input data
            await _validation.ValidateAsync<IdRequestDTOValidator, IdRequestDTO>(new IdRequestDTO { Id = id });

            // interaction with the database 
            var result = await _productRepository.GetById(id);

            if (result == null)
                throw new NotFoundException("There is no Product with this id.");

            return result;
        }

        public async Task<double> GetAmountByProductIds(IEnumerable<int> productIds)
        {
            // validating input data
            await _validation.ValidateAsync<IEnumerableIdsRequestDTOValidator, IEnumerableIdsRequestDTO>
                (new IEnumerableIdsRequestDTO { Ids = productIds });

            // checking data integrity
            if (!(await _productRepository.ExistAsync(productIds)))
                throw new NotFoundException("Some 'Product' does not exist.");

            // interaction with the database 
            var result = await _productRepository.GetAmountByProductIds(productIds);

            return result;
        }

        public async Task<int> Create(ProductCreateRequestDTO inputProduct)
        {
            // validating input data
            await _validation.ValidateAsync<ProductCreateRequestDTOValidator, ProductCreateRequestDTO>(inputProduct);

            // interaction with the database 
            var newProduct = new Product(inputProduct.Code, inputProduct.ProductName,
                                         inputProduct.Description, inputProduct.Price);
            
            await _productRepository.Add(newProduct);
            return newProduct.ProductId;
        }

        public async Task<bool> Update(ProductUpdateRequestDTO inputProduct)
        {
            // validating input data
            await _validation.ValidateAsync<ProductUpdateRequestDTOValidator, ProductUpdateRequestDTO>(inputProduct);

            // checking data integrity
            if (!(await _productRepository.ExistAsync(inputProduct.ProductId)))
                throw new NotFoundException("There is no Product with this id.");

            // interaction with the database 
            var existingProduct = await _productRepository.GetById(inputProduct.ProductId);

            existingProduct.ProductName = inputProduct.ProductName;
            existingProduct.Code = inputProduct.Code;
            existingProduct.Description = inputProduct.Description;
            existingProduct.Price = inputProduct.Price;

            await _productRepository.Update(existingProduct);

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            // validating input data
            await _validation.ValidateAsync<IdRequestDTOValidator, IdRequestDTO>(new IdRequestDTO { Id = id });
            
            // checking data integrity
            if (!(await _productRepository.ExistAsync(id)))
                throw new NotFoundException("There is no Product with this id.");
            if (await _orderProductRepository.ExistsByProductIdAsync(id))
                throw new BadRequestException("It is not possible to delete a Product that has been ordered.");
            
            // interaction with the database
            await _productRepository.Delete(id);

            return true;
        }
    }
}
