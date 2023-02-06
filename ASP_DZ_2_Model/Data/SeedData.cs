using ASP_DZ_2_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_2_Model.Data
{
    public static class SeedData
    {
        public static async Task Initialize(MobileContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (!context.Movies.Any())
            {
                //context.Movies.AddRange(

                //    new Movie
                //    {
                //        Name = "Men in Black",
                //        Director = "Barry Sonnenfeld",
                //        Genre = "action",
                //        Description = "ta-ta"
                //    },
                //     new Movie
                //     {
                //         Name = "Venom",
                //         Director = "Barry Sonnenfeld",
                //         Genre = "action",
                //         Description = "ta-ta"
                //     }
                //    );
                Movie movie = new Movie
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
                List<Movie> movies=new List<Movie>();
                List<Session> sessions=new List<Session>();
                Session session = new Session { TimeSession = "15:45", Movie = movie };
                Session session2 = new Session { TimeSession = "16:45", Movie = movie2 };

                List<Session> sessions1 = new List<Session>();
                List<Session> sessions2 = new List<Session>();

                sessions1.Add(session);
                sessions2.Add(session2);
                movie.SessionList = sessions1;
                movie2.SessionList = sessions2;

                movies.Add(movie);
                movies.Add(movie2);

               context.Movies.AddRange(movies);
               context.Sessions.AddRange(sessions1);
               context.Sessions.AddRange(sessions2);
                await context.SaveChangesAsync();
            }
        }
    }
}
