using DotnetNLayerProject.Core.Repositories;
using DotnetNLayerProject.Core.Services;
using DotnetNLayerProject.Core.UnitOfWorks;
using DotnetNLayerProject.Data;
using DotnetNLayerProject.Data.Repositories;
using DotnetNLayerProject.Data.UnitOfWorks;
using DotnetNLayerProject.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotnetNLayerProject.API.Filters;
using Microsoft.AspNetCore.Diagnostics;
using DotnetNLayerProject.API.DTOs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using DotnetNLayerProject.API.Extensions;

namespace DotnetNLayerProject.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)//Service'lerimizi eklediğimiz method
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<NotFoundFilter>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));//Service ismi hem namespace'in ismi hemde class'ın ismi
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"].ToString(),O=> {
                    O.MigrationsAssembly("DotnetNLayerProject.Data");
                });
            });

       
            services.AddControllers(o=> {
                o.Filters.Add(new ValidationFilter());
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)//middlewarelerimiz ekledigimiz method
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            /*<summary>
                Middleware, birbirleri ile iletişim halindeki uygulamalar arasında, tekrar eden görevleri gerçekleştirmek 
                amacıyla yer alan ara yazılımlardır. Tek başlarına bir işlem gerçekleştirmezler.
                Örneğin, dağıtılmış uygulamalar için iletişim ve veri yönetimini sağlayabilirler. 
                </summary>*/
            app.UseCustomException();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
