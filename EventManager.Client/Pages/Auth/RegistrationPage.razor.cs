using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.Auth
{
    public partial class RegistrationPage
    {
        [Inject]
        private IAuthService AuthService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        public string Title { get; set; } = "Registration";
        public RegistrationModel Model { get; set; }

        protected override void OnInitialized()
        {
            this.Model = new RegistrationModel
            {
                UserName = "",
                Email = "",
                FullName = "",
                Password = "",
                PasswordConfirm = ""
            };
        }

        protected void Navigate(string path)
        {
            this.NavigationManager.NavigateTo(path);
        }

        protected async Task SignUp()
        {
            await this.AuthService.Register(this.Model);
            this.Model = new RegistrationModel
            {
                UserName = "",
                Email = "",
                FullName = "",
                Password = "",
                PasswordConfirm = ""
            };
        }
    }
}