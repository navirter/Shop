using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class AddToCart
    {
        private ISession _session;
        private ApplicationDbContext _context;

        public AddToCart(ISession session, ApplicationDbContext context)
        {
            _session = session;
            _context = context;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }

        public class Response
        {

        }

        public async Task<bool> Do(Request request)
        {
            var stockOnHold = _context.StocksOnHold.Where(x => x.SessionId == _session.Id).ToList();
            //check if it really restores the amount  of stock after expiry
            var stockToHold = _context.Stock.Where(x => x.Id == request.StockId).FirstOrDefault();
            if (stockToHold.Qty < request.Qty)            
                return false;
            
            _context.StocksOnHold.Add(new StockOnHold
            {
                StockId = stockToHold.Id,
                Qty = request.Qty,
                SessionId = _session.Id,
                ExpiryDate = DateTime.Now.AddMinutes(20)
            });
            stockToHold.Qty -= request.Qty;
            foreach (var stock in stockOnHold)
                stock.ExpiryDate = DateTime.Now.AddMinutes(20);
            await _context.SaveChangesAsync();

            var cartList = new List<CartProduct>();
            string str = _session.GetString("cart");
            if (!string.IsNullOrEmpty(str))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(str);
            }
            if (cartList.Any(s => s.StockId == request.StockId))
            {
                cartList.Find(x => x.StockId == request.StockId).Qty += request.Qty;
            }
            else
            {
                cartList.Add(new CartProduct
                {
                    StockId = request.StockId,
                    Qty = request.Qty
                });
            }
            str = JsonConvert.SerializeObject(cartList);            
            _session.SetString("cart", str);
            return true;
        }
    }
}
