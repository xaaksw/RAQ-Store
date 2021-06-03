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


        [Required(ErrorMessage = "Please enter name"), MaxLength(30)]
        [Display(Name = "Product Name")]
        public string name { get; set; }

        [Display(Name = "Product Price")]
        public double? price { get; set; }


        
        [DataType(DataType.Upload)]
        [Display(Name = "Product image")]
        public string image { get; set; }
        


        [Required(ErrorMessage = "Please enter description"), MaxLength(200)]
        [Display(Name = "Product Description")]
        public string description { get; set; }

        public Category Category { get; set; }

        [ForeignKey("Category")]
        [Required(ErrorMessage = "pleae choose category")]
        [Display(Name = "Product Category")]
        public int? category_id { get; set; }
    }
}