using System;
using System.Text;
using AutoMapper;
using EventManager.Services.Profiles;
using EventManager.Services.Services;
using ManagerAPI.DataAccess;
using ManagerAPI.DataAccess.Entities;
using ManagerAPI.Services.Profiles;
using ManagerAPI.Services.Services;
using ManagerAPI.Services.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PlanManager.Services.Profiles;
using PlanManager.Services.Services;

namespace ManagerAPI.Backend
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .WithOrigins("https://localhost:5001");
                    });
            });

            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddLogging();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUtilsService, UtilsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventActionService, EventActionService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(PlanProfile));
            services.AddAutoMapper(typeof(EventProfile));

            services.AddDbContextPool<DatabaseContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("PlanManagerDb"));
            });

            services.AddIdentity<User, WebsiteRole>(o => o.Stores.MaxLengthForKeys = 128)
                .AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JwtSecret"]);

            services.AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}