using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using DotnetCore.Areas.Api.BLL;
using DotnetCore.BLL;
using DotnetCore.Models;
using DotnetCore.System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DotnetCore
{
#pragma warning disable 1591
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime. Use this method to add services to the container.
        //運行時將調用此方法。使用此方法將服務添加到容器。
        public void ConfigureServices(IServiceCollection services)
        {

            #region --Configuration--
            //弱型別
            //services.AddSingleton(provider => Configuration);
            //使用方式
            //使用這個型別 IConfiguration config
            //取值方式 config["DB:ConnectionStrings:DefaultConnection"]
            //強型別
            services.Configure<SystemSettingModel>(Configuration);
            //使用方式
            //使用這個型別 IOptions<Class Name> settings   Class Name=所使用的強型別
            //取值方式 settings.Value.DB.ConnectionStrings.DefaultConnection;
            #endregion

            #region --Session--
            services.AddDistributedMemoryCache();
            services.AddSession();
            #endregion
            #region --HttpContext--
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            services.AddHttpClient();

            services.AddTransient<ResultModel, ResultModel>();
            services.AddTransient<SessionHelper, SessionHelper>();
            services.AddTransient<TestBLL, TestBLL>();


            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();
            services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));
            services.AddTransient<OperationService, OperationService>();

            services.AddControllersWithViews().AddJsonOptions(
                options =>
                {
                    //如果回傳值為NULL會顯示 反之true
                    options.JsonSerializerOptions.IgnoreNullValues
                    = false;
                }
);
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
        }

        //This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //運行時將調用此方法。使用此方法來配置HTTP請求管道
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
                // HSTS的默認值為30天。您可能要針對生產方案更改此設置，請參見https://aka.ms/aspnetcore-hsts。
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();





            ////使用全域Middleware
            //app.UseMiddleware<GlobalMiddlewareTemplate>();
            //使用靜態包裝好的Middleware
            // app.UseFirstMiddleware();



            #region--Session--
            app.UseSession();
            #endregion
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
                endpoints.MapControllerRoute(
                    name: "Api",
                    pattern: "{area:exists}/{controller}/{action}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Backend",
                    pattern: "{area:exists}/{controller=Login}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                    );
            });
        }
    }
}
#pragma warning restore 1591