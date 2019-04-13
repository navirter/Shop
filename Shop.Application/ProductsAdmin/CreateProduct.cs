using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    public class CreateProduct
    {
        private ApplicationDbContext _context;

        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Do(Request request)
        {
            var product = new Product()
            {
                Name = request.Name, 
                Description = request.Description,
                Value = request.Value,
                PicPath = request.PicPath,
                Category = request.Category
            };
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
                PicPath = product.PicPath,
                Category = product.Category
            };
        }

        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            #region public string PicPath
            /// <summary>
            /// Returns empty string if the file doesn't exist
            /// </summary>
            public string PicPath
            {
                get
                {
                    if (!File.Exists(picPath))
                        picPath = "";
                    return picPath;
                }
                set
                {
                    picPath = value;
                }
            }
            string picPath = "";
            #endregion
            public string Category { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            #region public string PicPath
            public string PicPath
            {
                get
                {
                    if (!File.Exists(picPath))
                        picPath = "";
                    return picPath;
                }
                set
                {
                    picPath = value;
                }
            }
            string picPath = "";
            #endregion
            public string Category { get; set; }
        }
    }

}
