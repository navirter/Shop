using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Cart
{
    public  class GetCart
    {
        private ISession _session;
        private ApplicationDbContext _context;

        public GetCart(ISession session, ApplicationDbContext context)
        {
            _session = session;
            _context = context;
        }

        public class Response
        {
            public IEnumerable<ProductViewModel> ProductViewModels { get; set; }
            public int TotalPrice
            {
                get
                {
                    if (ProductViewModels != null)
                        return ProductViewModels.Sum(x => x.Qty * x.intValue);
                    else
                        return 0;
                }
            }
        }
        public class ProductViewModel
        {
            public string Name { get; set; }
            public string ValueToShow
            {
                get { return intValue.ToString("N2") + "$"; }
                set
                {
                    intValue = Convert.ToInt32(decimal.Parse(value.Replace("$", ""), System.Globalization.NumberStyles.Any));
                }
            }
            public int intValue { get; private set; }
            public int StockId { get; set; }
            public int Qty { get; set; }

        }

        public Response Do()
        { 
            string str = _session.GetString("cart");
            if (string.IsNullOrEmpty(str))
            {

                return new Response() { ProductViewModels = new List<ProductViewModel>() };
            }
            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(str);

            var productViewModels = _context.Stock
                .Include(x => x.Product)
                .Where(x => cartList.Any(s=> s.StockId == x.Id))
                .Select(x => new ProductViewModel
                {
                    Name = x.Product.Name,
                    ValueToShow = x.Product.Value.ToString("N2") + "$",
                    StockId = x.Id,
                    Qty = cartList.FirstOrDefault(s => s.StockId == x.Id).Qty
                })
                .ToList();
            var response = new Response()
            {
                ProductViewModels = productViewModels
            };
            return response;
        }
    }
}
