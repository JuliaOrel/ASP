using ASP_DZ_6_Books.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_6_Books.Models.ViewModels.BooksViewModels
{
    public class IndexBooksVM
    {
        public IEnumerable<BookDTO> Books { get; set; }
    }
       
}
