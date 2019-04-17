using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "Manager")]

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
        public IActionResult GetProducts()
        {
            var v = Ok(new GetProducts(_context).Do());
            return v;
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            var v = Ok(new Application.Products.GetCategories(_context).Do());
            return v;
        }

        [HttpGet("categories/{category}")]
        public IActionResult GetCategoryProducts(string category)
        {
            var v = Ok(new Application.Products.GetProductsByCategory(_context).Do(category));
            return v;
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var v = Ok(new GetProduct(_context).Do(id));
            return v;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody]CreateProduct.Request request)
        {
            var v = Ok(await new CreateProduct(_context).Do(request));
            return v;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var v = Ok(await new DeleteProduct(_context).Do(id));
            return v;
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateProduct([FromBody]UpdateProduct.Request request)
        {
            var v = Ok(await new UpdateProduct(_context).Do(request));
            return v;
        }
        
    }
}
