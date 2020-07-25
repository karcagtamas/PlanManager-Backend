using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Components.MyProfile
{
    public class UploadProfileImageBase : ComponentBase
    {
        [CascadingParameter]
        public ModalParameters Parameters { get; set; }

        [CascadingParameter]
        public BlazoredModal BlazoredModal { get; set; }

        [Inject] 
        private IUserService UserService { get; set; }

        [Inject]
        private IMatToaster Toaster { get; set; }

        [Inject]
        IModalService ModalService { get; set; }

        public int FormId { get; set; }

        private List<string> ImageExtensions { get; set; } = new List<string>
        {
            "image/jpg",
            "image/jpeg",
            "image/png",
            "image/bmp"
        };

        protected override void OnInitialized()
        {
            this.FormId = Parameters.Get<int>("FormId");
        }

        protected async Task FilesReady(IMatFileUploadEntry[] files)
        {
            try
            {
                var file = files.FirstOrDefault();
                if (file == null)
                {
                    return;
                }

                if (ImageExtensions.Contains(file.Type))
                {
                    await using var stream = new MemoryStream();
                    await file.WriteToStreamAsync(stream);
                    if (await UserService.UpdateProfileImage(stream.ToArray())) {
                        ModalService.Close(ModalResult.Ok<bool>(true));
                    }
                }
                else
                {
                    Toaster.Add("Invalid file extension. Please try again with a correct type.", MatToastType.Danger,
                        "My Profile Error");
                }
            }
            catch (Exception e)
            {
                Toaster.Add("Problem during the image uploading. Please try again later.", MatToastType.Danger,
                    "My Profile Error");
                Console.WriteLine(e);
            }
        }
    }
}