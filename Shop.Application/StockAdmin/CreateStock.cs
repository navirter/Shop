using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    public class CreateStock
    {
        private ApplicationDbContext _context;

        public CreateStock(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Do(Request request)
        {
            var stock = new Stock
            {
                ProductId = request.ProductId,
                Description = request.Description,
                Qty = request.Qty
            };
            try
            {
                _context.Stock.Add(stock);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return new Response
            {
                Id = stock.Id,
                Description = stock.Description,
                Qty = stock.Qty            
            };
        }

        public class Request
        {
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
    }
}
