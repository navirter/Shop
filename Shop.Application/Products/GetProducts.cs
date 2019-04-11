using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Products
{
    public class GetProducts
    {
        private ApplicationDbContext _context;

        public GetProducts(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel> Do() =>
            _context.Products
            .Include(s=> s.Stock)
            .Select(s => new ProductViewModel()
            {
                Name = s.Name,
                Description = s.Description,
                Value = s.Value.ToString("N2") + "$",  //1100.50 => 1,100.50 
                PriceMin = s.Stock.Min(x=> x.Price),
                PriceMax = s.Stock.Max(x=> x.Price),
                StockCount = s.Stock.Sum(x=> x.Qty)
            })
            .ToList();

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public decimal PriceMin { get; set; }
            public decimal PriceMax { get; set; }

            public int StockCount { get; set; }            
        }
    }
}
