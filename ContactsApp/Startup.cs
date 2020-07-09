using AutoMapper;
using ContactsApp.DTO;
using Employee.Domain;
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

namespace ContactsApp
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

            var dbConnectionString = Configuration.GetConnectionString("OrganisationDatabase");

            services.AddDbContext<OrganisationDbContext>(options =>
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

            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, OrganisationDbContext db)
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

                var routePrefix = "odata";
                string organisationControllerRouteName = "Organisations";
                routeBuilder.MapODataServiceRoute(
                    organisationControllerRouteName,
                    routePrefix,
                    GetOrganisationsEdmModel(organisationControllerRouteName));
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

        IEdmModel GetOrganisationsEdmModel(string routeName)
        {
            var odataBuilder = new ODataConventionModelBuilder();

            odataBuilder.EntityType<LinkDTO>()
                .HasKey(entity => entity.Id);

            odataBuilder.EntityType<EmployeeDTO>()
                .HasKey(entity => entity.Id)
                .HasMany(entity => entity.Links);

            odataBuilder.EntitySet<OrganisationDTO>(routeName).EntityType
                .HasKey(entity => entity.Id)
                .HasMany(entity => entity.Employees);

            return odataBuilder.GetEdmModel();
        }
    }
}
