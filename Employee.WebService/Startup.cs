using System.Linq;
using AutoMapper;
using Employee.Persistence;
using Employee.WebService.Filters;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Employee.WebService
{
    public class Startup
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddDebug(); });

        public IConfiguration Configuration { get; }

        private void ConfigureDatabase(IServiceCollection services)
        {
            var dbConnectionString = Configuration.GetConnectionString("ContactsDatabase");
            services.AddDbContext<EmployeeDbContext>(
                options => options.UseSqlServer(dbConnectionString).UseLoggerFactory(MyLoggerFactory),
                ServiceLifetime.Scoped);
        }

        private static void SetOutputFormatters(IServiceCollection services)
        {
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>())
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>())
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/odata"));
                }
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(c =>
            {
                c.DocumentFilter<OpenApiDocumentCustomIgnoreFilter>();
                c.OperationFilter<OdataOptionsIgnoreFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contacts API", Version = "v1" });
            });
            SetOutputFormatters(services);
        }

        private void ConfigureControllers(IServiceCollection services)
        {
            services.AddScoped<EmployeesService>();

            var mappingConfig = new MapperConfiguration(config => config.AddProfile(new MappingProfile()));
            services.AddSingleton(mapper => mappingConfig.CreateMapper());

            services.AddControllers(mvcOptions => mvcOptions.EnableEndpointRouting = false)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = ModelStateValidator.ValidateModelState);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            this.ConfigureDatabase(services);
            this.ConfigureControllers(services);

            services.AddOData();

            this.ConfigureSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EmployeeDbContext db)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API V1");
            });

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            db.Database.EnsureCreated();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                endpoints.EnableDependencyInjection();
                endpoints.Expand().Select().Filter();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                //if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
