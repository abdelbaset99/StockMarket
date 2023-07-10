using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using backend_asp.Models;
using backend_asp.Services;
using System.Collections.Generic;

namespace backend_asp.Pages
{
    public class StocksModel : PageModel
    {
        private readonly StockService _stocksService;
        
        [BindProperty]
        public IList<Stock> Stocks { get; set; } = new List<Stock>();

        public StocksModel(StockService stocksService)
        {
            _stocksService = stocksService;
        }
        public void OnGet()
        {
            Stocks = _stocksService.GetStocks();
        }
    }
}
