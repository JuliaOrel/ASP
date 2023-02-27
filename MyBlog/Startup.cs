using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlog.Authorization;
using MyBlog.Data;
using MyBlog.Data.Entitties;
using MyBlog.Services.EmailServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddIdentity<User, IdentityRole>(setupAction =>
             {
                 setupAction.User.RequireUniqueEmail = true;
                 setupAction.Password.RequireDigit = false;
                 setupAction.Password.RequireUppercase = false;
                 setupAction.Password.RequireLowercase = false;
                 setupAction.Password.RequireNonAlphanumeric = false;
                 setupAction.Password.RequiredLength = 1;
             })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    MyPolicies.PostsWriterAndAboveAccess,
                    policy => policy.RequireAssertion(context =>
                      {
                          return context.User.HasClaim(
                              claim => claim.Type == MyClaims.SuperAdmin ||
                              claim.Type == MyClaims.Admin ||
                              claim.Type == MyClaims.PostsWriter
                              );
                      }));
                options.AddPolicy(
                    MyPolicies.AdminAndAboveAccess,
                    policy => policy.RequireAssertion(context =>
                    {
                        return context.User.HasClaim(
                            claim => claim.Type == MyClaims.SuperAdmin ||
                            claim.Type == MyClaims.Admin);
                    }));
                options.AddPolicy(
                  MyPolicies.SuperAdminAccessOnly,
                  policy => policy.RequireAssertion(context =>
                  {
                      return context.User.HasClaim(
                          claim => claim.Type == MyClaims.SuperAdmin);
                  }));
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
                options.Cookie.IsEssential = true;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
