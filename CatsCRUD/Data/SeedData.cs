using CatsCRUD.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsCRUD.Data
{
    public static class SeedData
    {
        public static async Task Initialize(
            IServiceProvider serviceProvider,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration
            )
        {
            DbContextOptions<CatsContext> options =
                serviceProvider.GetRequiredService<DbContextOptions<CatsContext>>();


            using (CatsContext context = new CatsContext(options))
            {
                // context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                if (context.Cats.Any())
                {
                    return;
                }


                byte[] catImage1 = System.IO.File.ReadAllBytes($"{webHostEnvironment.WebRootPath}\\images\\cat1.jpg");
                byte[] catImage2 = System.IO.File.ReadAllBytes($"{webHostEnvironment.WebRootPath}\\images\\cat2.jpg");
                byte[] catImage3 = System.IO.File.ReadAllBytes($"{webHostEnvironment.WebRootPath}\\images\\cat3.jpg");
                byte[] catImage4 = System.IO.File.ReadAllBytes($"{webHostEnvironment.WebRootPath}\\images\\cat4.jpg");
                byte[] catImage5 = System.IO.File.ReadAllBytes($"{webHostEnvironment.WebRootPath}\\images\\cat5.jpg");

                Breed mainCoon = new Breed
                {
                    BreedName = "Maine Coon",
                };

                Breed mongrel = new Breed
                {
                    BreedName = "Mongrel",
                };

                Breed siamese = new Breed
                {
                    BreedName = "Siamese",
                };

                string loremIpsum = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa.\r\n\r\nCum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem.\r\n\r\nNulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis";
                Cat mainCoonCat = new Cat
                {
                    CatName = "Lucky",
                    Description = loremIpsum,
                    Gender = CatGender.Male,
                    Vaccinated = true,
                    Image = catImage1,
                    IsDeleted = false,
                    Breed = mainCoon,
                };

                Cat mongrelCat1 = new Cat
                {
                    CatName = "Abbey",
                    Description = loremIpsum,
                    Gender = CatGender.Female,
                    Vaccinated = false,
                    Image = catImage2,
                    IsDeleted = false,
                    Breed = mongrel,
                };

                Cat mongrelCat2 = new Cat
                {
                    CatName = "Alex",
                    Description = loremIpsum,
                    Gender = CatGender.Male,
                    Vaccinated = false,
                    Image = catImage3,
                    IsDeleted = false,
                    Breed = mongrel,
                };

                Cat mongrelCat3 = new Cat
                {
                    CatName = "Copper",
                    Description = loremIpsum,
                    Gender = CatGender.Male,
                    Vaccinated = false,
                    Image = catImage4,
                    IsDeleted = false,
                    Breed = mongrel,
                };

                Cat siameseCat = new Cat
                {
                    CatName = "Brandy",
                    Description = loremIpsum,
                    Gender = CatGender.Female,
                    Vaccinated = true,
                    Image = catImage5,
                    IsDeleted = false,
                    Breed = siamese,
                };
                await context.AddRangeAsync(
    mainCoonCat,
    mongrelCat1,
    mongrelCat2,
    mongrelCat3,
    siameseCat);

                await context.SaveChangesAsync();
            }
        }




    }
}
