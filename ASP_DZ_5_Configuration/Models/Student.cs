using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_5_Configuration.Models
{
    public class Student
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public List<string> Disciplines { get; set; }
    }
}
