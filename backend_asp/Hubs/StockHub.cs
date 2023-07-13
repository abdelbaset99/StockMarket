using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using backend_asp.Services;
using backend_asp.Models;
using backend_asp.Data;
using System.Linq;

namespace backend_asp.Hubs
{
    public class StockHub : Hub
    {
        private readonly StockService _stockService;
        private readonly StockContext _context;

        public StockHub(StockService stockService, StockContext context)
        {
            _context = context;
            _stockService = stockService;
        }
        
        public async Task UpdateStockPrices()
        {
            var stocks = await _stockService.GetStocksAsync();
            var random = new Random();

            foreach (var stock in stocks)
            {
                stock.Price = Math.Round(((decimal)(random.NextDouble() * 100)), 2);
                _context.Stocks.Update(stock);
            }

            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveStockPrices", stocks);
        }
    }
};