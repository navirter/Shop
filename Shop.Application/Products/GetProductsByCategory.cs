using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Shop.Application.Products
{
    public class GetProductsByCategory
    {
        private ApplicationDbContext _context;

        public GetProductsByCategory(ApplicationDbContext context)
        {
            _context = context;
        }

        public Response Do(string category)
        {
            var productsToShow = _context.Products
              .Include(s => s.Stock)
              .Where(s => !string.IsNullOrEmpty(s.Name))
              .Select(s => new ProductViewModel()
              {
                  Name = s.Name,
                  Description = s.Description,
                  DescriptionShort = s.Description.Substring(0, 30) + "...",
                  Value = s.Value.ToString("N2") + "$",  //1100.50 => 1,100.50 
                  StockCount = s.Stock.Sum(x => x.Qty),
                  Category = s.Category,
                  NameForRoute = "/Product/" + s.Name.Replace(" ", "-")
              })
              .ToList();
            Response r = new Response() { ProductViewModels = new List<ProductViewModel>() };
            foreach (var prod in productsToShow)
            {
                if (string.IsNullOrEmpty(prod.Category))
                    prod.Category = "Other";
                if (category == "All" || category == prod.Category)
                    r.ProductViewModels.Add(prod);
            }
            return r;
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string NameForRoute { get; set; }
            public string Description { get; set; }
            public string DescriptionShort { get; set; }
            public string Value { get; set; }
            public string Category { get; set; }
            public int StockCount { get; set; }
        }
        public class Response
        {
            public List<ProductViewModel> ProductViewModels { get; set; }
        }
    }
}
