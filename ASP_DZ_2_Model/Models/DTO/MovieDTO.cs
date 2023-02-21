using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_2_Model.Models.DTO
{
    public class MovieDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
