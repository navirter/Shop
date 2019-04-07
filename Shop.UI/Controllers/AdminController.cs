using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Database;
using Shop.Application.StockAdmin;
using Microsoft.AspNetCore.Authorization;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    //[Authorize(Policy = "Admin")] if added along with the previous line, it's neccessary to be both manager and admin to access things
    //so basically only the admin would be able to do things which is unwanted
    public class AdminController:Controller
    {
        private ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        
    }
}
