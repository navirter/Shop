using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.IO;
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

        public Response Do()
        {
            var productsToShow = _context.Products
              .Include(s => s.Stock)
              .Select(s => new ProductViewModel()
              {
                  Name = s.Name,
                  Description = s.Description,
                  Value = s.Value.ToString("N2") + "$",  //1100.50 => 1,100.50 
                  StockCount = s.Stock.Sum(x => x.Qty),
                  Category = s.Category
              })
              .ToList();
            foreach (var prod in productsToShow)
                if (string.IsNullOrEmpty(prod.Category))
                    prod.Category = "Other";
            Response r = new Response() { ProductViewModelsByCategory = new List<List<ProductViewModel>>() };
            foreach (var group in productsToShow.GroupBy(s => s.Category))            
                r.ProductViewModelsByCategory.Add(new List<ProductViewModel>(group));
            return r;
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public string Category { get; set; }
            public int StockCount { get; set; }
        }
        public class Response
        {
            public List<List<ProductViewModel>> ProductViewModelsByCategory { get; set; }
        }
    }
}
