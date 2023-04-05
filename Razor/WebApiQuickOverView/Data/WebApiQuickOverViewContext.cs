using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarsShared.Models;

namespace WebApiQuickOverView.Data
{
    public class WebApiQuickOverViewContext : DbContext
    {
        public WebApiQuickOverViewContext (DbContextOptions<WebApiQuickOverViewContext> options)
            : base(options)
        {
        }

        public DbSet<CarsShared.Models.ToDoItem> ToDoItems { get; set; }
    }
}
