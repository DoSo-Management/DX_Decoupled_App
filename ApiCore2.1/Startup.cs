using ApiViewModelMapper;
using BLL;
using DAL.BusinessObjects;
using DevExpress.Xpo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepoServices;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore2._1
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(Configuration.GetConnectionString("DXConnection"), DevExpress.Xpo.DB.AutoCreateOption.None);

            services.AddScoped<UnitOfWork>();

            var result = typeof(BlBase<>).Assembly
               .GetTypes()
               .Where(t => t.ParentTypes().Any(p => p.IsGenericType && p.GetGenericTypeDefinition() == typeof(BlBase<>)));

            foreach (var type in result)
            {
                services.AddScoped(type);
            }
            
            services.AddScoped<IApiService, ApiService>();
            services.AddScoped(typeof(IRepositoryID<>), typeof(DBRepository<>));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=swagger}/{id?}");
            });
        }
    }

    public static class ext
    {
        public static IEnumerable<Type> ParentTypes(this Type type)
        {
            foreach (var i in type.GetInterfaces())
            {
                yield return i;
                foreach (var t in i.ParentTypes())
                {
                    yield return t;
                }
            }

            if (type.BaseType != null)
            {
                yield return type.BaseType;
                foreach (var b in type.BaseType.ParentTypes())
                {
                    yield return b;
                }
            }
        }
    }
}
