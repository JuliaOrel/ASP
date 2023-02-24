using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Data.Entitties;
using MyBlog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Components
{
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly ApplicationContext _context;

        public CategoriesViewComponent(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IQueryable<Category> categories = _context.Categories;
            int categoryId = 0;

            if (Request.Query.ContainsKey("categoryId"))
            {
                int.TryParse(Request.Query["categoryId"].ToString(), out categoryId);
            }

            CategoriesDropDownListVM viewModel = new CategoriesDropDownListVM
            {
                Categories = await categories.ToListAsync(),
                CategoryId = categoryId
            };

            return View(viewModel);
        }

    }
}
