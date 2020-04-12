using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RAQ_Store.Models
{
    [Table("Product")]
    public class Product
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        public double? price { get; set; }

        [Required]
        public string image { get; set; }

        [Required]
        public string description { get; set; }

        public Category Category { get; set; }

        [ForeignKey("Category")]
        public int? category_id { get; set; }
    }
}