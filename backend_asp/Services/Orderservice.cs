using backend_asp.Data;
using backend_asp.Models;

namespace backend_asp.Services
{
    public class OrderService
    {
        private readonly StockContext _context = default!;

        public OrderService(StockContext context)
        {
            _context = context;
        }

        public IList<Order> GetOrders()
        {
            if (_context.Orders != null)
            {
                return _context.Orders.ToList();
            }
            return new List<Order>();
        }

        // public void AddStock(Stock Stock)
        // {
        //     if (_context.Stocks != null)
        //     {
        //         _context.Stocks.Add(Stock);
        //         _context.SaveChanges();
        //     }
        // }

        // public void DeleteStock(int id)
        // {
        //     if (_context.Stocks != null)
        //     {
        //         var Stock = _context.Stocks.Find(id);
        //         if (Stock != null)
        //         {
        //             _context.Stocks.Remove(Stock);
        //             _context.SaveChanges();
        //         }
        //     }            
        // } 
    }
}
