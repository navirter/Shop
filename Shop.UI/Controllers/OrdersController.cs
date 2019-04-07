using Microsoft.AspNetCore.Mvc;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.OrdersAdmin;
using Microsoft.AspNetCore.Authorization;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]

    public class OrdersController : Controller
    {

        private ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// //i removed status from Do's and function's inputs as i dunno why it was there
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult GetOrders() => Ok(new GetOrders(_context).Do());        

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id) => Ok(new GetOrder(_context).Do(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id) => Ok(await new UpdateOrder(_context).Do(id));

    }
}
