using FT_ProviderSys.Configs;
using FT_ProviderSys.DTOs;
using FT_ProviderSys.DTOs.Validators;
using FT_ProviderSys.Models;
using FT_ProviderSys.Repositories.Interfaces;
using FT_ProviderSys.Services.Interfaces;

namespace FT_ProviderSys.Services
{
    public class OrderService : IOrderService
    {
        public static List<Order> orderSample = new List<Order>();
        private static int nextId = 1;
        private readonly IOrderRepository _orderRepository;

        private readonly IValidationHelper _validation;

        public OrderService(IValidationHelper validation)
        {
            _validation = validation;
        }

        public List<Order> GetAll() => orderSample;

        public Order? GetById(int id)
        {
            return orderSample.FirstOrDefault(x => x.OrderId == id);
        }

        public async Task<int> Create(OrderCreateRequestDTO inputOrder)
        {
            var isValid = _validation.Validate<OrderCreateRequestDTOValidator, OrderCreateRequestDTO>(inputOrder);

            if (!isValid) throw new Exception();

            var newOrder = new Order(inputOrder.Code, inputOrder.ProviderId, inputOrder.Amount);

            //newOrder.OrderId = nextId++;
            //orderSample.Add(newOrder);
            await _orderRepository.Add(newOrder);
            return newOrder.OrderId;
        }

        public bool Update(OrderUpdateRequestDTO inputOrder)
        {
            var isValid = _validation.Validate<OrderUpdateRequestDTOValidator, OrderUpdateRequestDTO>(inputOrder);

            var existingOrder = orderSample.Find(x => x.OrderId == inputOrder.OrderId);
            if (existingOrder == null) return false;

            var index = orderSample.IndexOf(existingOrder);

            existingOrder.Code = inputOrder.Code;
            //existingOrder.ProductIds = inputOrder.ProductIds;
            existingOrder.ProviderId = inputOrder.ProviderId;
            existingOrder.Amount = inputOrder.Amount;

            orderSample[index] = existingOrder;

            return true;
        }

        public bool Delete(int id)
        {
            var existingOrder = orderSample.Find(x => x.OrderId == id);
            if (existingOrder == null) return false;

            orderSample.Remove(existingOrder);

            return true;
        }
    }
}
