using System.Collections.Generic;
using System.Threading.Tasks;
using backend_asp.Models;

public interface IStockService
{
    Task<List<Stock>> GetStocks();
}