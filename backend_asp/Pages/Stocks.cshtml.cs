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
        private readonly OrderService _ordersService;
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

        // public IActionResult OnPostBuyStock(int id)
        // {
        //     var stock = _stocksService.GetStock(id);
        //     if (stock == null)
        //     {
        //         return NotFound();
        //     }

        //     var buyRequest = new BuyRequest
        //     {
        //         StockName = Request.Form["StockName"],
        //         Quantity =  int.Parse(Request.Form["Quantity"]),
        //         BuyerName = Request.Form["BuyerName"]
        //     };

        //     _stocksService.buyStock(stock.ID, buyRequest.Quantity, stock.Price, buyRequest.BuyerName);

        //     return RedirectToPage();
        // }

    }
}
