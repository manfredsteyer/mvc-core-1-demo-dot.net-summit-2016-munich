using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using FlugDemo.Data;
using Microsoft.AspNet.Mvc.Formatters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace SummitDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen();

            services.AddMvc().AddMvcOptions(options => {

                var xmlInput = new XmlDataContractSerializerInputFormatter();
                var xmlOutput = new XmlDataContractSerializerOutputFormatter();

                options.InputFormatters.Add(xmlInput);
                options.OutputFormatters.Add(xmlOutput);
            }).AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            // services.AddSingleton; 
            // services.AddScoped

            services.AddTransient<IFlugRepository, FlugEfRepository>(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                // TODO
                app.UseExceptionHandler("/Error");
            }

            app.UseSwaggerGen();
            app.UseSwaggerUi();

            // /Flug/List/1 --> FlugController.List(int id)
            // / --> HomeController.Index(...)
            // /Home --> HomeController.Index(...)
            app.UseMvc(routes => {
                /*
                routes.MapRoute(
                    "conv",
                    "DETAIL/{id?}",
                    new { controller="Home", action="Detail" });
                */

                routes.MapRoute(
                        "default",
                        "{controller=Home}/{action=Index}/{id?}");
            });




        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
