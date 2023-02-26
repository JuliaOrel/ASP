using Medicines.Authorization;
using Medicines.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Medicines.Data
{
    public class SeedData
    {
        public static async Task Initialize(
    IServiceProvider serviceProvider,
    IWebHostEnvironment webHostEnvironment,
    IConfiguration configuration
    )
        {
            DbContextOptions<ApplicationContext> options =
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>();

            UserManager<User> userManager =
serviceProvider.GetRequiredService<UserManager<User>>();


            using (ApplicationContext context = new ApplicationContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                if (context.Medicines.Any())
                {
                    return;
                }
                string adminEmail = configuration.GetSection("AdminEmail").Value;
                string adminPassword = configuration.GetSection("AdminPassword").Value;

                if (await userManager.FindByNameAsync(adminEmail) == null)
                {
                    //1
                    User superAdmin = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        EmailConfirmed = true
                    };

                    IdentityResult result = await userManager.CreateAsync(superAdmin, adminPassword);
                    if (result.Succeeded)
                    {
                        //Add Claims
                        Claim claim1 = new Claim(MyClaims.PostsWriter, MyClaims.PostsWriter);
                        Claim claim2 = new Claim(MyClaims.Admin, MyClaims.Admin);
                        Claim claim3 = new Claim(MyClaims.SuperAdmin, MyClaims.SuperAdmin);

                        await userManager.AddClaimAsync(superAdmin, claim1);
                        await userManager.AddClaimAsync(superAdmin, claim2);
                        await userManager.AddClaimAsync(superAdmin, claim3);
                    }

                    // 2
                    User admin = new User
                    {
                        Email = "freid2011@gmail.com",
                        UserName = "freid2011@gmail.com",
                        EmailConfirmed = true
                    };

                    result = await userManager.CreateAsync(admin, adminPassword);
                    if (result.Succeeded)
                    {
                        //Add Claims
                        Claim claim1 = new Claim(MyClaims.PostsWriter, MyClaims.PostsWriter);
                        Claim claim2 = new Claim(MyClaims.Admin, MyClaims.Admin);

                        await userManager.AddClaimAsync(admin, claim1);
                        await userManager.AddClaimAsync(admin, claim2);
                    }

                    // 3
                    User postsWriter = new User
                    {
                        Email = "genry@gmail.com",
                        UserName = "genry@gmail.com",
                        EmailConfirmed = true
                    };

                    result = await userManager.CreateAsync(postsWriter, adminPassword);
                    if (result.Succeeded)
                    {
                        //Add Claims
                        Claim claim1 = new Claim(MyClaims.PostsWriter, MyClaims.PostsWriter);

                        await userManager.AddClaimAsync(postsWriter, claim1);
                    }
                }
                // End of Authorize And Claims **************************************************************
                else
                {
                    ILogger logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError("Cannot find the adminEmail");
                    return;
                }
                context.Medicines.AddRange(
                        new Medicine
                        {
                          Name="Nurofen",
                          Dosage="400 mg",
                          Category= "NSAIDs",
                          Producer= "Reckitt Benckiser"
                        },
                         new Medicine
                         {
                             Name = "Panadol",
                             Dosage = "500 mg",
                             Category = "Analgesics and antipyretics",
                             Producer = "GlaxoSmithKline"
                         }
                        );
                await context.SaveChangesAsync();
            }
        }
    }

}
        
