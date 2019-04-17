using Shop.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Shop.Application.Products
{
    public class GetCategories
    {
        private ApplicationDbContext _context;

        public GetCategories(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<string> Do()
        {
            var categories = _context.Products
                .GroupBy(s => s.Category)
                .Select(s => s.First().Category).ToList();
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i] == "")
                    categories[i] = "Other";
            }
            return categories;
        }

    }
}
