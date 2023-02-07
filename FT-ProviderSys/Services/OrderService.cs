using FT_ProviderSys.Configs;
using FT_ProviderSys.DTOs;
using FT_ProviderSys.DTOs.Validators;
using FT_ProviderSys.Exceptions;
using FT_ProviderSys.Models;
using FT_ProviderSys.Repositories.Interfaces;
using FT_ProviderSys.Services.Interfaces;

namespace FT_ProviderSys.Services
{
    public class OrderService : IOrderService
    {
        //public static List<Order> orderSample = new List<Order>();
        //private static int nextId = 1;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IOrderProductRepository _orderProductRepository;

        private readonly IValidationHelper _validation;

        public OrderService(
            IOrderRepository orderRepository, 
            IProductRepository productRepository,
            IProviderRepository providerRepository,
            IOrderProductRepository orderProductRepository,
            IValidationHelper validation)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _providerRepository = providerRepository;
            _orderProductRepository = orderProductRepository;
            _validation = validation;
        }

        public async Task<IEnumerable<Order>> GetAll() => await _orderRepository.GetAll();

        public async Task<Order?> GetById(int id)
        {
            // validating input data
            await _validation.ValidateAsync<IdRequestDTOValidator, IdRequestDTO>(new IdRequestDTO { Id = id });

            // interaction with the database 
            var result = await _orderRepository.GetById(id);

            if (result == null)
                throw new NotFoundException("There is no Order with this id.");

            return result;
        }

        public async Task<IEnumerable<Order>> GetByProviderId(int providerId)
        {
            // validating input data
            await _validation.ValidateAsync<IdRequestDTOValidator, IdRequestDTO>(new IdRequestDTO { Id = providerId });

            // checking data integrity
            if (!(await _providerRepository.ExistAsync(providerId)))
                throw new NotFoundException("There is no 'Provider' with this id.");
            if (!(await _orderRepository.ExistByProvider(providerId)))
                throw new BadRequestException("There is no 'Order' with this 'Provider'.");
            
            // interaction with the database
            var result = await _orderRepository.GetByProviderId(providerId);
            return result;
        }

        public async Task<int> Create(OrderCreateRequestDTO inputOrder)
        {
            // validating input data
            await _validation.ValidateAsync<OrderCreateRequestDTOValidator, OrderCreateRequestDTO>(inputOrder);

            List<OrderProduct> orderProducts = new List<OrderProduct>();

            // checking data integrity
            if (!(await _providerRepository.ExistAsync(inputOrder.ProviderId)))
                throw new NotFoundException("Its not possible to place an 'Order' with a non-existent Provider.");

            foreach (var product in inputOrder.Products)
                if (!(await _productRepository.ExistAsync(product.ProductId)))
                    throw new NotFoundException("Its not possible to place an 'Order' with a non-existent Product.");

            // interaction with the database 
            var amountOrder = await CalcAmount(inputOrder.Products);
            var newOrder = new Order(inputOrder.Code, inputOrder.ProviderId, amountOrder);

            await _orderRepository.Add(newOrder);

            foreach (var product in inputOrder.Products)
                orderProducts.Add(new OrderProduct(newOrder.OrderId, product.ProductId, product.Quantity));

            await _orderProductRepository.AddRange(orderProducts);

            return newOrder.OrderId;
        }

        public async Task<bool> Update(OrderUpdateRequestDTO inputOrder)
        {
            // validating input data
            await _validation.ValidateAsync<OrderUpdateRequestDTOValidator, OrderUpdateRequestDTO>(inputOrder);

            // checking data integrity
            if (!(await _orderRepository.ExistAsync(inputOrder.OrderId)))
                throw new NotFoundException("There is no Order with this id.");

            if (!(await _providerRepository.ExistAsync(inputOrder.ProviderId)))
                throw new NotFoundException("Its not possible to place an 'Order' with a non-existent Provider.");

            foreach (var product in inputOrder.Products)
                if (!(await _productRepository.ExistAsync(product.ProductId)))
                    throw new NotFoundException("Its not possible to place an 'Order' with a non-existent Product.");


            // interaction with the database 
            await _orderProductRepository.DeleteByOrderId(inputOrder.OrderId);
            var existingOrder = await _orderRepository.GetById(inputOrder.OrderId);

            List<OrderProduct> orderProducts = new List<OrderProduct>();

            foreach (var product in inputOrder.Products)
                orderProducts.Add(new OrderProduct(inputOrder.OrderId, product.ProductId, product.Quantity));

            await _orderProductRepository.AddRange(orderProducts);

            existingOrder.Code = inputOrder.Code;
            existingOrder.ProviderId = inputOrder.ProviderId;
            existingOrder.Amount = await CalcAmount(inputOrder.Products);

            await _orderRepository.Update(existingOrder);

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            // validating input data
            await _validation.ValidateAsync<IdRequestDTOValidator, IdRequestDTO>(new IdRequestDTO { Id = id});

            // checking data integrity
            if ( !(await _orderRepository.ExistAsync(id)) )
                throw new NotFoundException("There is no Order with this id.");

            // interaction with the database
            if ( await _orderProductRepository.ExistsByOrderIdAsync(id)) 
                await _orderProductRepository.DeleteByOrderId(id);

            await _orderRepository.Delete(id);

            return true;
        }

        private async Task<double> CalcAmount(IEnumerable<ProductQuantityRequestDTO> products)
        {
            double amount = 0;

            foreach (var product in products)
            {
                amount += await _productRepository.GetPrice(product.ProductId) * product.Quantity;
            }

            return amount;
        }
    }
}
