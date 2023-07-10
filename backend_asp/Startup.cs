using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using backend_asp.Data;
using backend_asp.Services;

namespace backend_asp
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
            services.AddControllers();

            services.AddDbContext<StockContext>();

            services.AddScoped<StockService>();

            services.AddCors(options =>
                {
                    options.AddPolicy("AllowAngularDevServer",
                    builder =>
                        {
                            builder.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                });


        }

        // public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        // {
        //     if (env.IsDevelopment())
        //     {
        //         app.UseDeveloperExceptionPage();
        //         app.UseHsts();
        //     }

        //     app.UseHttpsRedirection();

        //     app.UseRouting();

        //     app.UseCors("AllowAngularDevServer");

        //     app.UseAuthorization();

        //     app.UseEndpoints(endpoints =>
        //     {
        //         endpoints.MapControllers();
        //     });
        // }
    }
}