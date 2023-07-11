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

        [BindProperty]
        public IList<Order> Orders { get; set; } = new List<Order>();
        public StocksModel(StockService stocksService, OrderService orderService)
        {
            _stocksService = stocksService;
            _ordersService = orderService;

        }
        public void OnGet()
        {
            Stocks = _stocksService.GetStocks();
            Orders = _ordersService.GetOrders();
        }

        public IActionResult OnPostBuyStock(int id)
        {
            var stock = _stocksService.GetStock(id);
            if (stock == null)
            {
                return NotFound();
            }

            var buyRequest = new BuyRequest
            {
                StockName = Request.Form["StockName"],
                Quantity =  int.Parse(Request.Form["Quantity"]),
                BuyerName = Request.Form["BuyerName"]
            };

            _stocksService.buyStock(stock.ID, buyRequest.Quantity, stock.Price, buyRequest.BuyerName);

            return RedirectToPage();
        }

    }
}
