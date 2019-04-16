using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TalkAspNetCoreDemo.Data;
using TalkAspNetCoreDemo.Models.Configuration;
using TalkAspNetCoreDemo.Models.Services;
using TalkAspNetCoreDemo.PageRoute;

namespace TalkAspNetCoreDemo
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<TalkDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TalkConnectionString"));
            });

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddRazorPagesOptions(options =>
                     {
                         options.Conventions.AddPageRoute("/Courses/Index", "/Corsi/{handler?}/{value?}");
                         options.Conventions.AddPageRoute("/Courses/Detail", "/DettagliCorso/{id}/{title?}/");
                         options.Conventions.AddPageRoute("/Courses/Edit", "/ModificaCorso/{id}/{title?}/");
                         options.Conventions.Add(new CustomPageRouteModelConvention());
                     });

            services.AddTransient<IHeaderService, HeaderService>();
            services.AddTransient<ICourseService, CourseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var _configurationSection = Configuration.GetSection("DataSeedConfiguration");
            var _dataSeedConfiguration = new DataSeedConfiguration()
            {
                RebootDatabase = _configurationSection.GetValue<bool>("RebootDatabase"),
                ImagePreviewPath = _configurationSection.GetValue<string>("ImagePreviewPath"),
                ImageThumbPath = _configurationSection.GetValue<string>("ImageThumbPath")
            };
            DataSeed.Seed(service, env, _dataSeedConfiguration);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
