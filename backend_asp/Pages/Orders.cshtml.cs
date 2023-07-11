using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using backend_asp.Models;
using backend_asp.Services;
using System.Collections.Generic;

namespace backend_asp.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly OrderService _ordersService;

        [BindProperty]
        public IList<Order> Orders { get; set; } = new List<Order>();
        public OrdersModel(OrderService orderService)
        {
            _ordersService = orderService;

        }
        public void OnGet()
        {
            Orders = _ordersService.GetOrders();
        }


    }
}
