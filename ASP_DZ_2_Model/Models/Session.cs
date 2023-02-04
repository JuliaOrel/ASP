using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_2_Model.Models
{
    public class Session
    {
        public int ID { get; set; }
        public string TimeSession { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
