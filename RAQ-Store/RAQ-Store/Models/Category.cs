using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RAQ_Store.Models
{
    [Table("Category")]
    public class Category
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string name { get; set; }


        public int? number_of_products { get; set; }
    }
}