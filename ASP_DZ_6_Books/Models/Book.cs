using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_6_Books.Models
{
    public class Book
    {
        public string NameBook { get; set; }
        public string FIOAuthor { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public int YearIssue { get; set; }
    }
}
