using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_asp.Models;
using backend_asp.Data;

namespace backend_asp.Repositories
{
    public class OrderRepo: IOrderRepo
    {
        public OrderContext _orderContext { get; } 
        public OrderRepo(OrderContext orderContext)
        {
            _orderContext = orderContext ?? throw new ArgumentNullException(nameof(orderContext));
        }
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var orders = _orderContext.Orders.OrderByDescending(o => o.ID).ToList();

            return await Task.FromResult(orders);
        }

        public async Task<Order> MakeOrderAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            await _orderContext.Orders.AddAsync(order);
            await _orderContext.SaveChangesAsync();
            return order;
        }
    }
}