using Shop.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Shop.Application.ProductsAdmin
{
    public class GetProduct
    {
        private ApplicationDbContext _context;

        public GetProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public ProductViewModel Do(int id) =>
            _context.Products.ToList().Where(x=> x.Id == id)
            .Select(s => new ProductViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Value = s.Value,
                Category = s.Category
            }).FirstOrDefault();

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string Category { get; set; }
        }
    }
}
