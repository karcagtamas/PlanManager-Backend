using System;
using System.Threading.Tasks;
using EventManager.Frontend.Data.Models;
using EventManager.Frontend.Services;
using Microsoft.AspNetCore.Components;

namespace EventManager.Frontend.Pages.Auth
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        private IAuthService AuthService { get; set; }
        
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        public string Title { get; set; } = "Login";
        public LoginModel Model { get; set; }

        public bool DisplayError = false;
        public string ErrorMessage = "";

        protected override void OnInitialized()
        {
            Model = new LoginModel
            {
                UserName = "",
                Password = ""
            };
            base.OnInitialized();
        }

        protected async Task SignIn()
        {
            var result = await AuthService.SignIn(Model);
            if (result.IsSuccess)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                ErrorMessage = result.Message;
                DisplayError = true;
            }
        }
    }
}