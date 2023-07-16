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
using backend_asp.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using backend_asp.Configurations;
using backend_asp.Services;
using System.IdentityModel.Tokens.Jwt;

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

            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            services.AddControllers();

            services.AddDbContext<StockContext>();

            services.AddDbContext<OrderContext>();

            services.AddDbContext<UserContext>();

            services.AddScoped<StockService>();

            services.AddScoped<OrderService>();

            services.AddScoped<UserService>();

            // services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            //     .AddEntityFrameworkStores<UserContext>();

            // services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //     .AddEntityFrameworkStores<UserContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                // options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                // options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                // var secret = Encoding.UTF8.GetBytes(Configuration["JwtConfig:Secret"] ?? throw new InvalidOperationException("JwtConfig:Secret is null"));
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    SaveSigninToken = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    // ValidIssuer = "http://localhost",
                    ValidIssuer = Configuration["JwtConfig:Issuer"],
                    // ValidAudience = "localhost",
                    ValidAudience = Configuration["JwtConfig:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfig:Secret"] ?? throw new InvalidOperationException("JwtConfig:Secret is null")))
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

            // services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();


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