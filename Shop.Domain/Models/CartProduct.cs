using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
    //stored in the session
    public class CartProduct
    {
        public int StockId { get; set; }
        public int Qty { get; set; }
    }
}
