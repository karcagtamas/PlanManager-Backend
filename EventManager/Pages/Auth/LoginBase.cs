using System;
using System.Threading.Tasks;
using EventManager.Data.Models;
using Microsoft.AspNetCore.Components;

namespace EventManager.Pages.Auth
{
    public class LoginBase : ComponentBase
    {
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
            try
            {
                // await AuthService.Login(Model);
                NavigationManager.NavigateTo("/");
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                DisplayError = true;
            }
        }
    }
}