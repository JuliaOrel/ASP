using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesCitiesShared.DTO
{
    public class CountryDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Country Name")]
        public string Name { get; set; }
        [Range(1,999)]
        [Display(Name = "Country Code")]
        public int Code { get; set; }
        
    }
}
