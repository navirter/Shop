using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.ViewComponents
{
    /*this is what should be used for reusable components throughout the web app*/
    public class CartViewComponent : ViewComponent
    {
        private ApplicationDbContext _context;

        public CartViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string view ="Default")
        {
            var cart = new GetCart(HttpContext.Session, _context).Do();
            return View(view, cart);
        }
    }
}
