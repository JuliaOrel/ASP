using ASP_DZ_2_Model.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_2_Model.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            DbContextOptions<MoviesContext> options = serviceProvider.GetRequiredService<DbContextOptions<MoviesContext>>();
            using (MoviesContext context = new MoviesContext(options))
            {
                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                if (context.Movies.Any())
                {
                    return;
                }



                Movie movie1 = new Movie
                {
                    Name = "Men in black",
                    Director = "Barry Sonnenfeld",
                    Genre = "action",
                    Description = "ta-ta"
                };
                Movie movie2 = new Movie
                {
                    Name = "Venom",
                    Director = "Barry Sonnenfeld",
                    Genre = "action",
                    Description = "ta-ta"
                };
                List<Session> sessionList1 = new List<Session>
                {
                    new Session {TimeSession="15:45", Movie=movie1},
                    new Session {TimeSession="16:45", Movie=movie1}

                };
                List<Session> sessionList2 = new List<Session>
                {
                    new Session {TimeSession="17:45", Movie=movie2},
                    new Session {TimeSession="18:45", Movie=movie2}

                };
                movie1.Sessions = sessionList1;
                movie2.Sessions = sessionList2;

                List<Movie> movies = new List<Movie>
                {
                    movie1,
                    movie2
                };

                context.Movies.AddRange(movies);
                context.Sessions.AddRange(sessionList1);
                context.Sessions.AddRange(sessionList2);
                await context.SaveChangesAsync();
            }
        
            
        }
    }
}
