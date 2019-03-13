using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebFramework.Services;
using Framework.Context;
using Framework.Models.UserManagement;
using WebFramework.SignalR.Hubs;
using Microsoft.AspNetCore.Http;
using Framework.Repositories.Infrastructor;
using Framework.Utils;
using System;
using Framework.Repositories.UserManagement;
using Framework.Services.ManageService.UserManagement;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using WebCore.Services.Share;

namespace WebFramework
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
            services.AddDbContext<FrameworkDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<FrameworkDbContext>()
            .AddDefaultTokenProviders();


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.Scan(scan =>
                scan.FromAssemblyOf<IApplicationUserRepository>()
                    .AddClasses(classes => classes.Where(i => i.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            services.Scan(scan =>
                scan.FromAssemblyOf<UserRolesManagementService>()
                    .AddClasses(classes => classes.Where(i => i.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            services.AddSignalR();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(60000);
                options.Cookie.HttpOnly = true;
            });



            //services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            //services.AddScoped(typeof(IService<>), typeof(BaseService<>));

            services.AddMvc();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            ResolveDepedencyInjection.ServiceProvider = services.BuildServiceProvider();

            MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
           mc.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            AppDependencyResolver.Init(app.ApplicationServices);

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<GroupHub>("/groupHub");
                routes.MapHub<NotificationHub>("/notificationHub");
            });
        }
    }
}
