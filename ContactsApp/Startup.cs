using Employee.DTO;
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
using Microsoft.OData.Edm;

namespace ContactsApp
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
            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            var dbConnectionString = Configuration.GetConnectionString("OrganisationDatabase");

            services.AddDbContext<OrganisationDbContext>(options =>
                options.UseSqlServer(dbConnectionString), ServiceLifetime.Scoped);

            services.AddDbContext<OrganisationQueryDbContext>(options =>
                options.UseSqlServer(dbConnectionString), ServiceLifetime.Singleton);
            
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
                routeBuilder.Select().Filter();
                
                var routePrefix = "odata";
                string employeesRouteName = "EmployeesQuery";
                routeBuilder.MapODataServiceRoute(
                    employeesRouteName, routePrefix, GetEmployeesEdmModel(employeesRouteName));

                string organisationsRouteName = "OrganisationsQuery";
                routeBuilder.MapODataServiceRoute(
                    organisationsRouteName, routePrefix, GetOrganisationsEdmModel(organisationsRouteName));
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

        IEdmModel GetEmployeesEdmModel(string routeName)
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<PlainEmployeeDTO>(routeName)
                .EntityType.HasKey(entity => entity.Id);

            return odataBuilder.GetEdmModel();
        }
        IEdmModel GetOrganisationsEdmModel(string routeName)
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<OrganisationDTO>(routeName)
                .EntityType.HasKey(entity => entity.Id);

            return odataBuilder.GetEdmModel();
        }
    }
}
