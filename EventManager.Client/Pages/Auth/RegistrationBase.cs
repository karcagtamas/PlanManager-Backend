using System.Threading.Tasks;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.Models;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Pages.Auth
{
    public class RegistrationBase : ComponentBase
    {
        [Inject] 
        private IAuthService AuthService { get; set; }
        
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        
        [Inject]
        private IHelperService HelperService { get; set; }
        
        [Inject]
        private IMatToaster Toaster { get; set; }
        
        public string Title { get; set; } = "Registration";
        public RegistrationModel Model { get; set; }

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

        protected void Navigate(string path)
        {
            NavigationManager.NavigateTo(path);
        }
        
        protected async Task SignUp()
        {

            await AuthService.Register(Model);
            Model = new RegistrationModel
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