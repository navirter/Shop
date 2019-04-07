using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        //const string products = "products";//used to equal "products" when was a part of AdminController.cs
        // but it's no longer neeeded as it's a separate file
        //all routs in this file used this prefix


        private ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }        

        [HttpGet("")]
        public IActionResult GetProducts() => Ok(new GetProducts(_context).Do());

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_context).Do(id));

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody]CreateProduct.Request request) => Ok(await new CreateProduct(_context).Do(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) => Ok(await new DeleteProduct(_context).Do(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateProduct([FromBody]UpdateProduct.Request request) => Ok(await new UpdateProduct(_context).Do(request));
        
    }
}
