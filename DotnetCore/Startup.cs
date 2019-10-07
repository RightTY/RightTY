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
        //�B��ɱN�եΦ���k�C�ϥΦ���k�N�A�ȲK�[��e���C
        public void ConfigureServices(IServiceCollection services)
        {

            #region --Configuration--
            //�z���O
            //services.AddSingleton(provider => Configuration);
            //�ϥΤ覡
            //�ϥγo�ӫ��O IConfiguration config
            //���Ȥ覡 config["DB:ConnectionStrings:DefaultConnection"]
            //�j���O
            services.Configure<SystemSettingModel>(Configuration);
            //�ϥΤ覡
            //�ϥγo�ӫ��O IOptions<Class Name> settings   Class Name=�ҨϥΪ��j���O
            //���Ȥ覡 settings.Value.DB.ConnectionStrings.DefaultConnection;
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
                    //�p�G�^�ǭȬ�NULL�|��� �Ϥ�true
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
                //�]�mSwagger JSON�MUI���������|
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #endregion
        }

        //This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //�B��ɱN�եΦ���k�C�ϥΦ���k�Ӱt�mHTTP�ШD�޹D
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
                // HSTS���q�{�Ȭ�30�ѡC�z�i��n�w��Ͳ���ק�惡�]�m�A�аѨ�https://aka.ms/aspnetcore-hsts�C
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();





            ////�ϥΥ���Middleware
            //app.UseMiddleware<GlobalMiddlewareTemplate>();
            //�ϥ��R�A�]�˦n��Middleware
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
                    // url: �ݰt�X SwaggerDoc �� name�C "/swagger/{SwaggerDoc Version}/swagger.json"
                    url: "/Swagger/v1/swagger.json",
                    // name: �Ω� Swagger UI �k�W����ܤ��P������ SwaggerDocument ��ܦW�٨ϥΡC
                    name: "My API"
                );
                //���|swagger/Index.html
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