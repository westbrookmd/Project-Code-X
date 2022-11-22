using DataAccess.Data;
using DataAccess.DbAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectCodeX.Data;
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
            builder.Services.AddDbContext<ProjectCodeXContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<User, IdentityRole>
                (options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ProjectCodeXContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options => { });
            builder.Services.AddAuthorization(options => 
            { 
                //options.AddPolicy("Admin", policy => policy.RequireRole("Admin")); 
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
            builder.Services.AddTransient<IEventData, EventData>();
            builder.Services.AddTransient<EventViewModel>();
            builder.Services.AddTransient<NewsViewModel>();
            builder.Services.AddTransient<UserViewModel>();
            builder.Services.AddTransient<DonationViewModel>();
            builder.Services.AddTransient<ContactViewModel>();
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
                UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var adminRole = roleManager.FindByNameAsync("Admin").Result;
                if (adminRole == null)
                {
                    adminRole = new IdentityRole("Admin");
                    var role = roleManager.CreateAsync(adminRole).Result;
                }
                if (app.Environment.IsDevelopment())
                {
                    try
                    {
                        var adminUser = new User()
                        {
                            UserName = builder.Configuration.GetSection("DevelopmentUsers").GetValue<string>("AdminEmailAddress"),
                            Email = builder.Configuration.GetSection("DevelopmentUsers").GetValue<string>("AdminEmailAddress"),
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                        };
                        var userInDB = userManager.FindByNameAsync(adminUser.Email).Result;

                        if (userInDB == null)
                        {
                            //create admin user
                            var password = builder.Configuration.GetSection("DevelopmentUsers").GetValue<string>("AdminPassword");
                            var user = userManager.CreateAsync(adminUser, password).Result;
                            if (user.Succeeded)
                            {
                                var result = userManager.AddToRoleAsync(adminUser, "Admin").Result;
                            }
                            else
                            {
                                throw new Exception(user.Errors.ToString());
                            }
                        }
                        else
                        {
                            var roles = userManager.GetRolesAsync(userInDB).Result;
                            if (!userManager.IsInRoleAsync(userInDB, "Admin").Result)
                            {
                                var example = userManager.AddToRoleAsync(userInDB, adminRole.Name).Result;
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
                    if (!userManager.IsInRoleAsync(professorUser, "Admin").Result)
                    {
                        var result = userManager.AddToRoleAsync(professorUser, "Admin").Result;
                    }
                }
            }
        }
    }
}