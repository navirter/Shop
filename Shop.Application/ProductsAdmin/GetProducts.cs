﻿using Shop.Database;
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
                Category = s.Category
            });

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string NameForRoute { get { return Name.Replace(" ", "-"); } }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string Category { get; set; }
        }
    }
}
