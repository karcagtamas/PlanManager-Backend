using System.Threading.Tasks;
using EventManager.Frontend.Data.Models;
using EventManager.Frontend.Services;
using Microsoft.AspNetCore.Components;

namespace EventManager.Frontend.Pages.Auth
{
    public class RegistrationBase : ComponentBase
    {
        public string Title { get; set; } = "Registration";
        public RegistrationModel Model { get; set; }
        public bool DisplayError = false;
        public string ErrorMessage = "";

        protected override void OnInitialized()
        {
            Model = new RegistrationModel
            {
                UserName = "",
                Email = "",
                FullName = "",
                Password = "",
                PasswordConfirm = ""
            };
            base.OnInitialized();
        }
        
        protected async Task SignUp()
        {
            /*var result = await AuthService.SignIn(Model);
            if (result.IsSuccess)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                ErrorMessage = result.Message;
                DisplayError = true;
            }*/
        }
    }
}