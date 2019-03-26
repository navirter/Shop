using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Cart
{
    public class AddToCart
    {
        private ISession _session;

        public AddToCart(ISession session)
        {
            _session = session;
        }

        public class Request
        {
            public int StockId{ get; set; }
            public int Qty { get; set; }
        }

        public class Response
        {

        }

        public void Do(Request request)
        {
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
        }
    }
}
