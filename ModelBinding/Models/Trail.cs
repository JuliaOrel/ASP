using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBinding.Models
{
    public class Trail
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public float TrailLength { get; set; }
    }
}
