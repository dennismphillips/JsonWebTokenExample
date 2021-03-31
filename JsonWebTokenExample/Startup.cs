using JsonWebTokenExample.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
namespace JsonWebTokenExample
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
            services.AddControllers().AddNewtonsoftJson();
            services.AddOptions();

            //string connection = Configuration["LibraryConnectionString"];
            //services.AddTransient<IDbConnection>((sp) => new MySqlConnection(connection));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = " Dennis' Core Web Services Demonstration", Version = "v1" });
            });
            services.AddSwaggerGenNewtonsoftSupport(); //This is an explicit opt in. Must be placed after the add swagger gen statement

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddTransient<IJsonWebTokenLogic, JsonWebTokenLogic>();
            //services.AddTransient<ILibraryLogic, LibraryLogic>();
            //services.AddScoped<ILibraryQuery, LibraryQuery>();

            //TODO: This is a good spot to add startup logging. Something to capture all of the configuration variables
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");

                //TODO:  Someday make a nice css file to make the page prettier
                //c.InjectStylesheet("/swagger-ui/custom.css");
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            //TODO: Someday add JWT authentication with user backed database
            //app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
