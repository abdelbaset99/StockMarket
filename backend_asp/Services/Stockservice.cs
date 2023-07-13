using backend_asp.Data;
using backend_asp.Models;

namespace backend_asp.Services
{
    public class StockService
    {
        private readonly StockContext _context = default!;

        public StockService(StockContext context) 
        {
            _context = context;
        }

        public async Task<List<Stock>> GetStocksAsync()
        {
            return await Task.Run(() => GetStocks().ToList());
        }
        
        public IList<Stock> GetStocks()
        {
            if(_context.Stocks != null)
            {
                return _context.Stocks.ToList();
            }
            return new List<Stock>();
        }

        public Stock GetStock(int id)
        {
            if(_context.Stocks != null)
            {
                return _context.Stocks.Find(id);
            }
            return new Stock();
        }

        public Stock GetStockByName(string name)
        {
            if(_context.Stocks != null)
            {
                return _context.Stocks.Where(s => s.Name == name).FirstOrDefault();
            }
            return new Stock();
        }

        public void buyStock(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
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
