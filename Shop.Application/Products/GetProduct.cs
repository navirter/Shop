using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Products
{
    public class GetProduct
    {
        private ApplicationDbContext _context;

        public GetProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public ProductViewModel Do(string name) =>
            _context.Products
            .Include(s => s.Stock)
            .Where(s => s.Name == name)
            .Select(s => new ProductViewModel()
            {
                Name = s.Name,
                Description = s.Description,
                Value = s.Value.ToString("N2") + "$",  //1100.50 => 1,100.50
                Stock = s.Stock.Select(x => new StockViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    InStock = x.Qty > 0
                })
            })
            .FirstOrDefault();

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public bool InStock { get; set; }
        }
    }
}
