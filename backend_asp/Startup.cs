using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using backend_asp.Models;
using backend_asp.Services;
using backend_asp.Data;
using backend_asp.Hubs;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;


namespace backend_asp
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });
                // (Optional) Configure the comments path for the Swagger JSON and UI   
            });

            services.AddControllers();

            services.AddDbContext<StockContext>();

            services.AddDbContext<OrderContext>();

            services.AddDbContext<UserContext>();

            services.AddScoped<StockService>();

            services.AddScoped<OrderService>();

            services.AddScoped<UserService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"] ?? "defaultKey"))
                };
            });

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

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            }
            app.UseSwagger();

            // Enable middleware to serve Swagger UI (HTML, JS, CSS, etc.)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
                // (Optional) Set the Swagger UI to the app's root URL
                c.RoutePrefix = string.Empty;
            });

            // loggerFactory.AddConsole();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAngularDevServer");

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<StockHub>("/stockhub");
            });
        }
    }
}