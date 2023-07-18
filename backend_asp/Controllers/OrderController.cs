using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_asp.Data;
using backend_asp.Models;
using backend_asp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace backend_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderContext context, OrderService orderService)
        {
            // _context = context;
            _orderService = orderService;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

       
        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("makeorder")]
        public async Task<ActionResult<Order>> MakeOrder(Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            var newOrder = await _orderService.MakeOrderAsync(order);
            if (newOrder == null)
            {
                return BadRequest();
            }
            return Ok(newOrder);
        }
    }
}
