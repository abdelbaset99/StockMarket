using backend_asp.Data;
using backend_asp.Models;
using backend_asp.Repositories;

namespace backend_asp.Services
{
    public class StockService
    {
        private readonly IStockRepo _stockRepo = default!;


        public StockService(IStockRepo stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public async Task<IEnumerable<Stock>> GetStocksAsync()
        {
            return await _stockRepo.GetStocksAsync();
        }
        
          }    
}
