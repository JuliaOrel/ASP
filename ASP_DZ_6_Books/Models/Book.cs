using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_6_Books.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Display(Name = "Book")]
        public string NameBook { get; set; }
        [Display(Name = "Author")]
        public string FIOAuthor { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public int YearIssue { get; set; }
        public byte[] Image { get; set; }
    }
}
