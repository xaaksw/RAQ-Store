using RAQ_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAQ_Store.ViewModels
{
    public class ProductCategory
    {
        public List<Product> Product { get; set; }
        public List<Category> Category { get; set; }
        public Product MyProduct { get; set; }
        public Category MyCategory { get; set; }
        public  List<Cart> cart { get; set; }
        public Cart Mycart { get; set; }
    }
}