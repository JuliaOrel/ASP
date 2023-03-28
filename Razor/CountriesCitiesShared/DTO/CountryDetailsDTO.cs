using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesCitiesShared.DTO
{
    
    public class CountryDetailsDTO
    {
       
        public IEnumerable<CityDTO> Cities { get; set; }
    }
}
