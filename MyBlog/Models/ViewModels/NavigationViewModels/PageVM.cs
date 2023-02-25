using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models.ViewModels.NavigationViewModels
{
    public class PageVM
    {
        public int PageNumber { get; }
        public int TotalPages { get; set; }
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
        public PageVM(int count, int pageSize,int pageNumber)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
