using NewsSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Data
{
    public class SeedData
    {
        public static void Initialize(NewsBlogContext context)
        {
            // Look for any students.
            if (context.NewsOne.Any())
            {
                return;   // DB has been seeded
            }

            var news = new NewsOne[]
            {
                new NewsOne{Title="North Korean state media", Text="North Korean state media weighed in on Saturday " +
                "on allegations that Western nations were involved in blasts that damaged Russia's undersea " +
                "Nord Stream gas pipelines last year, in the " +
                "latest move by Pyongyang to express support for Moscow", Date=new DateTime(2023,3,4)},
                new NewsOne{Title="Earthquake in Turkey", Text="Many people died", Date=new DateTime(2023,2,6)}
            };
            context.NewsOne.AddRange(news);
            context.SaveChanges();

        }

    }
}
