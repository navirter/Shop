﻿using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Shop.Application.ProductsAdmin
{
    public class DeleteProduct
    {
        private ApplicationDbContext _context;

        public DeleteProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Do(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        
    }

}
