using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.StockAdmin
{
    public class GetStock
    {
        private ApplicationDbContext _context;

        public GetStock(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel> Do()
        {
            var stock = _context.Products
                .Include(x=> x.Stock)
                .Select(x => new ProductViewModel
                {
                   Id = x.Id,
                   Description = x.Description,
                   Stock = x.Stock.Select(y => new StockViewModel
                   {
                        Id = y.Id,
                        Description = y.Description,
                        Qty = y.Qty,
                        Price = y.Price
                   })
                })
                .ToList();

            return stock;
        }

        public IEnumerable<StockViewModel> Do(int productId)
        {
            //var product = _context.Products.FirstOrDefault(s => s.Id == productId);
            var stock = _context.Stock
                .Where(s => s.ProductId == productId)
                .Select(y => new StockViewModel
                {
                    Id = y.Id,
                    Description = y.Description,
                    Qty = y.Qty,
                    Price = y.Price
                })
                .ToList();

            return stock;
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
            public int Price { get; set; }
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}
