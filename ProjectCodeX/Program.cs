using DataAccess.Data;
using DataAccess.DbAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectCodeX.Data;
using ProjectCodeX.Managers;
using ProjectCodeX.Models;

namespace ProjectCodeX
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection") ?? throw new InvalidOperationException("Connection string 'DatabaseConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>
                (options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
            builder.Services.AddTransient<IEventData, EventData>();
            builder.Services.AddTransient<EventMgr>();
            builder.Services.AddTransient<EventViewModel>();
            var app = builder.Build();

            CreateAdminUsers(builder, app);


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

        private static void CreateAdminUsers(WebApplicationBuilder builder, WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var adminRole = roleManager.FindByNameAsync("Admin").Result;
                if (adminRole == null)
                {
                    adminRole = new IdentityRole("Admin");
                    roleManager.CreateAsync(adminRole);
                }
                if (app.Environment.IsDevelopment())
                {
                    try
                    {
                        var adminUser = new IdentityUser()
                        {
                            UserName = builder.Configuration.GetSection("DevelopmentUsers").GetValue<string>("AdminEmailAddress"),
                            Email = builder.Configuration.GetSection("DevelopmentUsers").GetValue<string>("AdminEmailAddress"),
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                        };
                        var userInDB = userManager.FindByEmailAsync(adminUser.Email).Result;

                        if (userInDB == null)
                        {
                            //create admin user
                            var password = builder.Configuration.GetSection("DevelopmentUsers").GetValue<string>("AdminPassword");
                            var user = userManager.CreateAsync(adminUser, password).Result;
                            if (user.Succeeded)
                            {
                                userManager.AddToRoleAsync(adminUser, "Admin");
                            }
                            else
                            {
                                throw new Exception(user.Errors.ToString());
                            }
                        }
                        else
                        {
                            var roles = userManager.GetRolesAsync(userInDB).Result;
                            if (!roles.Contains("Admin"))
                            {
                                userManager.AddToRoleAsync(adminUser, "Admin");
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }


                }
                var professorUser = userManager.FindByEmailAsync("admin@cpt275.beausanders.org").Result;
                if (professorUser != null)
                {
                    var roles = userManager.GetRolesAsync(professorUser).Result;
                    if (!roles.Contains("Admin"))
                    {
                        userManager.AddToRoleAsync(professorUser, "Admin");
                    }
                }
            }
        }
    }
}