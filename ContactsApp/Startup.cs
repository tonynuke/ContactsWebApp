using System.Text.Json;
using AutoMapper;
using Contacts.WebService.DTO;
using Contacts.WebService.DTO.Employee;
using Contacts.WebService.DTO.Link;
using Employee.Persistence;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using Newtonsoft.Json.Serialization;

namespace Contacts.WebService
{
    public class Startup
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddDebug(); });

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            var dbConnectionString = Configuration.GetConnectionString("OrganizationDatabase");

            services.AddDbContext<EmployeeDbContext>(options =>
            {
                options.UseSqlServer(dbConnectionString).UseLoggerFactory(MyLoggerFactory);
            }, ServiceLifetime.Scoped);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers(mvcOptions =>
                mvcOptions.EnableEndpointRouting = false);

            //services.AddMvc()
            //    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EmployeeDbContext db)
        {
            db.Database.EnsureCreated();

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller}/{action=Index}/{id?}");
            //});

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Expand().Select().Filter();

                const string routePrefix = "odata";
                const string controllerRouteName = "Employees";
                routeBuilder.MapODataServiceRoute(
                    controllerRouteName,
                    routePrefix,
                    GetOrganizationsEdmModel(controllerRouteName));
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        IEdmModel GetOrganizationsEdmModel(string routeName)
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EnableLowerCamelCase();

            odataBuilder.EntityType<LinkDTO>()
                .HasKey(entity => entity.Id);

            odataBuilder.EntitySet<EmployeeDTO>(routeName).EntityType
                .HasKey(entity => entity.Id)
                .HasMany(entity => entity.Links);

            return odataBuilder.GetEdmModel();
        }
    }
}
