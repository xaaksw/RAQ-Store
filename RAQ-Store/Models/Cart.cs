using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RAQ_Store.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Product")]
        public int product_id { get; set; }
        public Product Product  { get; set; }


        [Key, Column(Order = 2)]
        [DataType(DataType.DateTime)]
        public DateTime added_at { get; set; }


    }
}