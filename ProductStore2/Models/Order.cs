using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore2.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Display(Name ="User's name")]
        public string UserName { get; set; }
        public string ContactPhone { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
