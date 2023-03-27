using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesCitiesShared.DTO
{
    public class CityDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="City Name")]
        [StringLength(60, MinimumLength =2)]
        public string Name { get; set; }
        [Range(0,1000_000_000_0)]
        public int Population { get; set; }
        [Display(Name="Air Pollution Degree")]
        [DisplayFormat(NullDisplayText ="No checked")]
        public AirPollutionDegree AirPollutionDegree { get; set; }
        //public bool IsDeleted { get; set; }
        public int CountryId { get; set; }
       
    }
}
