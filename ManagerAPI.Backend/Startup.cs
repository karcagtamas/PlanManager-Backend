using System;
using System.IO;
using System.Text;
using AutoMapper;
using CsomorGenerator.Profiles;
using CsomorGenerator.Services;
using CsomorGenerator.Services.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;
using ManagerAPI.Backend.Middlewares;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Services.Common.CSV;
using ManagerAPI.Services.Common.Excel;
using ManagerAPI.Services.Common.Mail;
using ManagerAPI.Services.Common.PDF;
using ManagerAPI.Services.Profiles;
using ManagerAPI.Services.Services;
using ManagerAPI.Services.Services.Interfaces;
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
using MovieCorner.Services.Profiles;
using MovieCorner.Services.Services;
using MovieCorner.Services.Services.Interfaces;
using PlanManager.Services.Profiles;
using PlanManager.Services.Services;
using PlanManager.Services.Services.Interfaces;

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
                            .WithOrigins("https://localhost:5001", "http://localhost:8080", "http://192.168.1.6:8080");
                    });
            });

            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new UserProfile());
                x.AddProfile(new PlanProfile());
                x.AddProfile(new NotificationProfile());
                x.AddProfile(new FriendProfile());
                x.AddProfile(new MessageProfile());
                x.AddProfile(new NewsProfile());
                x.AddProfile(new TaskProfile());
                x.AddProfile(new WorkingDayProfile());
                x.AddProfile(new WorkingFieldProfile());
                x.AddProfile(new WorkingDayTypeProfile());
                x.AddProfile(new MovieProfile());
                x.AddProfile(new BookProfile());
                x.AddProfile(new SeriesProfile());
                x.AddProfile(new GenderProfile());
                x.AddProfile(new CsomorProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<ExceptionHandler>();

            /*services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(EventProfile));
            services.AddAutoMapper(typeof(PlanProfile));*/

            services.AddLogging();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUtilsService, UtilsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IWorkingDayService, WorkingDayService>();
            services.AddScoped<IWorkingDayTypeService, WorkingDayTypeService>();
            services.AddScoped<IWorkingFieldService, WorkingFieldService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ISeriesService, SeriesService>();
            services.AddScoped<ISeasonService, SeasonService>();
            services.AddScoped<IEpisodeService, EpisodeService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ICsvService, CsvService>();
            services.AddScoped<IExcelService, ExcelService>();
            services.AddScoped<IMovieCategoryService, MovieCategoryService>();
            services.AddScoped<IMovieCommentService, MovieCommentService>();
            services.AddScoped<ISeriesCategoryService, SeriesCategoryService>();
            services.AddScoped<ISeriesCommentService, SeriesCommentService>();
            services.AddScoped<IGeneratorService, GeneratorService>();
            services.AddScoped<IPDFService, PDFService>();

            new CustomAssemblyLoadContext().LoadUnmanagedLibrary($"{Directory.GetCurrentDirectory()}/dll/libwkhtmltox.dll");
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContextPool<DatabaseContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("ManagerDb"));
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

            app.UseMyExceptionHandler();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}