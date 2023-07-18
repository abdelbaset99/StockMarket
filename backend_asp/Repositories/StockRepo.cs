using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_asp.Models;
using backend_asp.Data;

namespace backend_asp.Repositories
{
    public class StockRepo: IStockRepo
    {
        public StockContext _stockContext { get; }

        public StockRepo(StockContext stockContext)
        {
            _stockContext = stockContext;
        }

        public async Task<IEnumerable<Stock>> GetStocksAsync()
        {
            return await Task.FromResult(_stockContext.Stocks.ToList());
        }        
    }
}