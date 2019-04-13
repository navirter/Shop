using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace Shop.Application.ProductsAdmin
{
    public class UpdateProduct
    {
        private ApplicationDbContext _context;

        public UpdateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Do(Request request)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == request.Id);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Value = request.Value;
            product.PicPath = request.PicPath;
            product.Category = request.Category;

            await _context.SaveChangesAsync();
            return new Response()
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
            public int Id { get; set; }
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
    }

}
