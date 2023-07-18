using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_asp.Models;
using backend_asp.Data;

namespace backend_asp.Repositories
{
    public interface IStockRepo
    {
        StockContext _stockContext { get; }
        Task<IEnumerable<Stock>> GetStocksAsync();
    }
}