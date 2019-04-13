using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Products;
using Shop.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        

        public GetProducts.Response Products { get; set; }
        public List<string> Categories { get; set; }

        public void OnGet()
        {
            Products = new GetProducts(_context).Do();
            Categories = new List<string>();
            Products.ProductViewModelsByCategory.ForEach(s => Categories.Add(s.First().Category));
        }
    }
}
