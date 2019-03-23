using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
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
            var cartProduct = new CartProduct
            {
                StockId = request.StockId,
                Qty = request.Qty
            };
            string str = JsonConvert.SerializeObject(cartProduct);

            //TODO: append the cart

            _session.SetString("cart", str);
        }
    }
}
