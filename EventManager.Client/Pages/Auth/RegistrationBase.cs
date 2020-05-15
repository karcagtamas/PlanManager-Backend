using System;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services;
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
            try
            {
                var result = await AuthService.Register(Model);
                if (result.IsSuccess)
                {
                    Toaster.Add(result.Message, MatToastType.Success, "Registration Successful");
                }
                else
                {
                    Toaster.Add(result.Message, MatToastType.Danger, "Registration Error");
                }
                Model = new RegistrationModel
                {
                    UserName = "",
                    Email = "",
                    FullName = "",
                    Password = "",
                    PasswordConfirm = ""
                };
            }
            catch (Exception e)
            {
                Toaster.Add(HelperService.ConnectionIsUnreachable(), MatToastType.Danger, "Registration Error");
                Console.WriteLine(e);
            }
        }
    }
}