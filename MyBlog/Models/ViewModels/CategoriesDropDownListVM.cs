﻿using MyBlog.Data.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models.ViewModels
{
    public class CategoriesDropDownListVM
    {
        public IEnumerable<Category> Categories { get; set; }
        public int CategoryId { get; set; }
    }
}
