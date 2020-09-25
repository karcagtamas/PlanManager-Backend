using System.Threading.Tasks;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.Models;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.Auth
{
    public partial class LoginPage
    {
        [Inject]
        private IAuthService AuthService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected string Title { get; set; } = "Login";
        protected LoginModel Model { get; set; }

        protected override void OnInitialized()
        {
            Model = new LoginModel
            {
                UserName = "",
                Password = ""
            };
        }

        protected void Navigate(string path)
        {
            NavigationManager.NavigateTo(path);
        }

        protected async Task SignIn()
        {
            if (!string.IsNullOrEmpty(await AuthService.Login(Model)))
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}