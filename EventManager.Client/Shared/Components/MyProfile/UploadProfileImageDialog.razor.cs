using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EventManager.Client.Shared.Components.MyProfile
{
    public partial class UploadProfileImageDialog
    {
        [CascadingParameter] public ModalParameters Parameters { get; set; }

        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }

        [Inject] private IUserService UserService { get; set; }

        [Inject] private IMatToaster Toaster { get; set; }

        [Inject] private IModalService ModalService { get; set; }

        private int FormId { get; set; }
        private IMatFileUploadEntry File { get; set; }

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

            ((ModalService)ModalService).OnConfirm += OnConfirm;
        }

        private async void OnConfirm()
        {
            if (this.File == null)
            {
                return;
            }

            if (ImageExtensions.Contains(this.File.Type))
            {
                try
                {
                    await using var stream = new MemoryStream();
                    await this.File.WriteToStreamAsync(stream);
                    if (await UserService.UpdateProfileImage(stream.ToArray()))
                    {
                        ModalService.Close(ModalResult.Ok<bool>(true));
                        ((ModalService)ModalService).OnConfirm -= OnConfirm;
                    }
                }
                catch (Exception e)
                {
                    Toaster.Add("Problem during the image uploading. Please try again later.", MatToastType.Danger,
                        "My Profile Error");
                    Console.WriteLine(e);
                }
            }
            else
            {
                Toaster.Add("Invalid file extension. Please try again with a correct type.", MatToastType.Danger,
                    "My Profile Error");
            }
        }

        protected void FilesReady(IMatFileUploadEntry[] files)
        {
            this.File = files.FirstOrDefault();
        }
    }
}