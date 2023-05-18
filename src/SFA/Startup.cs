using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.Entities;
using SFA.Services;
using Microsoft.Extensions.Hosting;
using Newtonsoft;
using SFA.Services.TimedJobs;

namespace SFA
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            HostingEnvironment = env;

        }
        public IWebHostEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    //options.LoginPath = new PathString("/apex/login");
                    options.AccessDeniedPath = new PathString("/http-errors/access-denied");
                });

            services.AddResponseCompression();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddDbContext<SFADBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DataConnection")));

            services.AddControllers().AddNewtonsoftJson();

            services.AddScoped<IMenuGroupService, MenuGroupService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleMenuService, RoleMenuService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IAttributeService, AttributeService>();
            services.AddScoped<IChurchService, ChurchService>();
            services.AddScoped<IChurchAccommodationService, ChurchAccommodationService>();
            services.AddScoped<IMacroScheduleService, MacroScheduleService>();
            services.AddScoped<IServiceTypeService, ServiceTypeService>();
            services.AddScoped<IChurchServiceTimeService, ChurchServiceTimeService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAccomodationBookService, AccomodationBookService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IReportService, ReportService>();
           // services.AddScoped<IHostedService, HQEmailTwoWeeks>();

            //Adding Timer Jobs
            services.AddHostedService<HQEmailTwoWeeks>();

            //This is what was used for 2.1 Version
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddRazorPages();
            
           
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseRequestLocalization();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Login}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapControllers();


            });
          //  app.UseMvc(routes =>
           // {
            //    routes.MapRoute(
            //        name: "default",
           //         template: "{controller=Login}/{action=Index}/{id?}");
           // });
        }
    }
}
