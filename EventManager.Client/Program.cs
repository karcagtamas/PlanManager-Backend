using Blazored.LocalStorage;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using MatBlazor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace EventManager.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            // builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddScoped(
                sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IHelperService, HelperService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IFriendService, FriendService>();
            builder.Services.AddScoped<IModalService, ModalService>();
            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IWorkingDayService, WorkingDayService>();
            builder.Services.AddScoped<IWorkingFieldService, WorkingFieldService>();
            builder.Services.AddScoped<IWorkingDayTypeService, WorkingDayTypeService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IGenderService, GenderService>();
            builder.Services.AddScoped<ISeriesService, SeriesService>();
            builder.Services.AddScoped<ISeasonService, SeasonService>();
            builder.Services.AddScoped<IEpisodeService, EpisodeService>();
            builder.Services.AddScoped<IMovieCategoryService, MovieCategoryService>();
            builder.Services.AddScoped<IMovieCommentService, MovieCommentService>();
            builder.Services.AddScoped<ISeriesCategoryService, SeriesCategoryService>();
            builder.Services.AddScoped<ISeriesCommentService, SeriesCommentService>();
            builder.Services.AddScoped<IGeneratorService, GeneratorService>();

            if (builder.HostEnvironment.IsDevelopment())
            {
                ApplicationSettings.BaseUrl = "https://localhost:8000";
                ApplicationSettings.BaseApiUrl = ApplicationSettings.BaseUrl + "/api";
            }

            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });

            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }

        private static IConfiguration GetConfiguration()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("appsettings.json"))
            using (var reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<IConfiguration>(reader.ReadToEnd());
            }
        }
    }
}