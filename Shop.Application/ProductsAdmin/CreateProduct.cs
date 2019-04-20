using Microsoft.AspNetCore.Hosting;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    public class CreateProduct
    {
        private ApplicationDbContext _context;
        private IHostingEnvironment _appEnvironment;

        public CreateProduct(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;            
        }

        public async Task<Response> Do(Request request)
        {
            var product = new Product()
            {
                Name = request.Name, 
                Description = request.Description,
                Value = request.Value,
                Category = request.Category
            };
            if (_context.Products.FirstOrDefault(s => s.Name == product.Name) != null)
            {
                Console.WriteLine("A product with such name already exists");
                return null;
            }
            _context.Products.Add(product);

            string rootPath = _appEnvironment.WebRootPath;
            string nameForRoute = product.Name.Replace(" ", "-");
            string imgPath = rootPath + "/images/ProductsImages/" + nameForRoute + ".img";
            if (File.Exists(imgPath))
                File.Delete(imgPath);
            //TODO: write a new picture
            //< img src = "~/images/Queek Headtaker.jpg" style = "max-width:256px; max-height:256px" />
            

            await _context.SaveChangesAsync();

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
                Category = product.Category
            };
        }

        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string Category { get; set; }
            
        }
        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string Category { get; set; }
        }
    }

}
