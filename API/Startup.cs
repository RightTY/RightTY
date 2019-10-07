using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace API
{
#pragma warning disable  CS1591
    public class Startup
    {

        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ResultModel, ResultModel>();
            services.AddControllers();

            #region --Swagger 5.0.0-rc3 --
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1",
                    Description = "This is WENHUA API.",
                    //TermsOfService = new Uri(""),
                    Contact = new OpenApiContact
                    {
                        Name = "WENHUA",
                        //Url = new Uri("")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "CC BY-NC-SA 4.0",
                        Url = new Uri("https://creativecommons.org/licenses/by-nc-sa/4.0/")
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                //設置Swagger JSON和UI的註釋路徑
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #endregion


            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.IgnoreNullValues
                             = false;
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            #region --Swagger 5.0.0-rc3 --
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    // url: 需配合 SwaggerDoc 的 name。 "/swagger/{SwaggerDoc Version}/swagger.json"
                    url: "/Swagger/v1/swagger.json",
                    // name: 用於 Swagger UI 右上角選擇不同版本的 SwaggerDocument 顯示名稱使用。
                    name: "My API"
                );
                //路徑swagger/Index.html
                c.RoutePrefix = "swagger";
            });
            #endregion
            
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
#pragma warning restore CS1591
}
