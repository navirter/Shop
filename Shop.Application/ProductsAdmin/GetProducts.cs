﻿using Shop.Database;
using System;
using System.Collections.Generic;
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
                Name = s.Name,
                Description = s.Description,
                Value = s.Value
            });

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
