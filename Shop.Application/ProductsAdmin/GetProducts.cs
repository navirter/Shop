using Shop.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Shop.Application.ProductsAdmin
{
    public class GetProducts
    {
        private ApplicationDbContext _context;

        public GetProducts(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel> Do() =>
            _context.Products.ToList().Select(s => new ProductViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Value = s.Value,
                PicPath = s.PicPath,
                Category = s.Category
            });

        public class ProductViewModel
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
