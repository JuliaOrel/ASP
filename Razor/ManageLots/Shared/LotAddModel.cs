using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageLots.Shared
{
   public  class LotAddModel
    {
        [Required]
        public Currency Currency { get; set; }
        [Required]
        [Range(1,100_000)]
        public int Amount { get; set; }
        [Required]
        [StringLength(100, MinimumLength =1)]
        public string Seller { get; set; }
    }
}
