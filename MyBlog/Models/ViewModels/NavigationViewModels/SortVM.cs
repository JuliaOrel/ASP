using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models.ViewModels.NavigationViewModels
{
    public class SortVM
    {
        public SortState Current { get; set; }
        public SortVM(SortState sortOrder)
        {
            Current = sortOrder;
        }

    }
    public enum SortState
    {
        TitleAsc,
        TitleDesc,
        DescriptionAsc,
        DescriptionDesc,
        CategoryAsc,
        CategoryDesc,
        CreatedAsc,
        CreatedDesc
    }
}