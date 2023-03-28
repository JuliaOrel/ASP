using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesCitiesShared.DTO
{
    public class CityDetailsDTO: CityDTO
    {
        [Required]
        [Display(Name="Country Name")]
        //[JsonPropertyOrder(10)]
        public string CountryName { get; set; }
        [Display(Name = "Country Code")]
        public int CountryCode { get; set; }
    }
}
