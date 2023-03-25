using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace WebApiQuickOverView.Data
{
    public class WebApiQuickOverViewContext : DbContext
    {
        public WebApiQuickOverViewContext (DbContextOptions<WebApiQuickOverViewContext> options)
            : base(options)
        {
        }

        public DbSet<Shared.Models.ToDoItem> ToDoItems { get; set; }
    }
}
