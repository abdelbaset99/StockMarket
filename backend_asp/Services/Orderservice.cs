using backend_asp.Data;
using backend_asp.Models;
using backend_asp.Repositories;

namespace backend_asp.Services
{
    public class OrderService
    {
        private readonly IOrderRepo _orderRepo = default!;

        public OrderService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _orderRepo.GetOrdersAsync();
        }

        public async Task<Order> MakeOrderAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            var newOrder = await _orderRepo.MakeOrderAsync(order);
            return newOrder;
        }
    }
}
