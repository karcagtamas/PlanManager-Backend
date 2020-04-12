using System;
using System.Threading.Tasks;
using EventManager.Data.Enums;
using EventManager.Data.Models;
using Microsoft.AspNetCore.Components;

namespace EventManager.Pages.Auth
{
    public class RegistrationBase : ComponentBase
    {
        public string Title { get; set; } = "Registration";
        public RegistrationModel Model { get; set; }
        public bool DisplayMessage = false;
        public string Message = "";
        public AlertType MessageType { get; set; }

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
            try
            {
                // await AuthService.Registration(Model);
                MessageType = AlertType.Success;
                Message = "Registration was success";
                DisplayMessage = true;
            }
            catch (Exception e)
            {
                Message = e.Message;
                DisplayMessage = true;
                MessageType = AlertType.Error;
            }

        }
    }
}