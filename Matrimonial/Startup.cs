
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Matrimonial.context;
using Matrimonial.Interface;
using Matrimonial.Concrete;

using  Microsoft.EntityFrameworkCore;




using Matrimonial.Filter;
using Microsoft.AspNetCore.Identity;
using Matrimonial.Model;
using Matrimonial.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Matrimonial
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(env.ContentRootPath)
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            //    .AddEnvironmentVariables();
            //Configuration = builder.Build();

        }

       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            // services.Add(new ServiceDescriptor(typeof(DatabaseContext), new DatabaseContext(Configuration.GetConnectionString("DefaultConnection"))));

            // services.AddMvc().AddJsonOptions
            //(a => a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            // services.AddEntityFrameworkSqlServer().AddDbContext<DatabaseContext>();

            // services.AddScoped<IAdventureWorksRepository, AdventureWorksRepository>();

            // services.AddOptions();

            // services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // services.AddSingleton<IConfiguration>(Configuration);


            services.AddApplicationInsightsTelemetry(Configuration);
            //services.AddMvc();

            services.AddMvc(options =>
            {
                options.Filters.Add(new ApiValidationFilterAttribute()); // an instance
               
            });


            var setting=Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IMatrimoneyRepository<>), typeof (Matrimonial.Concrete.MatrimonyRepository<>));
            //services.AddIdentity<RegisterUser, IdentityRole>()
            //    .AddEntityFrameworkStores<DatabaseContext>();



            //    services.AddIdentity<ApplicationUser, IdentityRole>()
            //.AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddDefaultTokenProviders();

            //    services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/LogIn");

            services.AddCors();

            //
            services.AddTransient<DataSeeder>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            // Enable Dual Authentication 
            services.AddAuthentication()
              .AddCookie(cfg => cfg.SlidingExpiration = true)
              .AddJwtBearer(cfg =>
              {
                  cfg.RequireHttpsMetadata = false;
                  cfg.SaveToken = true;

                  cfg.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidIssuer = Configuration["Tokens:Issuer"],
                      ValidAudience = Configuration["Tokens:Issuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                  };

              });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataSeeder seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

           

            //app.UseStatusCodePagesWithReExecute("/error/{0}");
            //app.UseExceptionHandler("/error/500");
            

            app.UseMiddleware<ErrorWrappingMiddleware>();
            // app.UseStaticFiles();

            



            //app.UseMvc(routes =>
            //{
            //    routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}/{action}");
            //});
           
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                          name: "default",
                          template: "{controller=Home}/{action=Index}/{id?}");
            });

            seeder.SeedAsync().Wait();
        }



    }


}

