using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_6_Books.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
