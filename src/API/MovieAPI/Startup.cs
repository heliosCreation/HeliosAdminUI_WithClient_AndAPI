using API.Application;
using API.Application.Contracts.Identity;
using API.Identity;
using API.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MovieAPI.Services;

namespace MovieAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddIdentityService(Configuration);
            services.AddPersistence(Configuration);

            services.AddHttpContextAccessor();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Movie - WebApi",
                        Description = "This Api will be responsible for managing the movies and completing claim bases.",
                        Contact = new OpenApiContact
                        {
                            Name = "HeliosCreation",
                            Email = "reliableDevelopment@hotmail.com",
                        }
                    });
            });


            services.AddScoped<ILoggedInUserService, LoggedInUserService>();

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
