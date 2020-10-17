using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

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
            this.Model = new LoginModel
            {
                UserName = "",
                Password = ""
            };
        }

        protected void Navigate(string path)
        {
            this.NavigationManager.NavigateTo(path);
        }

        protected async Task SignIn()
        {
            if (!string.IsNullOrEmpty(await this.AuthService.Login(this.Model)))
            {
                this.NavigationManager.NavigateTo("/");
            }
        }
    }
}