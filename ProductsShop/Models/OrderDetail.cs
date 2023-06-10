﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsShop.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }                
        [Required]
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        //Product
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        //Order
        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}