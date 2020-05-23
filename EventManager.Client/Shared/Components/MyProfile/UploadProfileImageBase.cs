using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EventManager.Client.Services;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Components.MyProfile
{
    public class UploadProfileImageBase : ComponentBase
    {
        [Parameter] public bool DialogIsOpen { get; set; }

        [Parameter] public EventCallback<bool> Response { get; set; }

        [Inject] private IUserService UserService { get; set; }

        [Inject] private IMatToaster Toaster { get; set; }

        [Inject] private IHelperService HelperService { get; set; }

        private List<string> ImageExtensions { get; set; } = new List<string>
        {
            "image/jpg",
            "image/jpeg",
            "image/png",
            "image/bmp"
        };


        protected async Task Save()
        {
            await Response.InvokeAsync(true);
        }

        protected async Task Cancel()
        {
            await Response.InvokeAsync(false);
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
                    try
                    {
                        var result = await UserService.UpdateProfileImage(stream.ToArray());
                        if (result.IsSuccess)
                        {
                            Toaster.Add("Successfully updated User", MatToastType.Success, "My Profile");
                            await Response.InvokeAsync(true);
                        }
                        else
                        {
                            Toaster.Add(result.Message, MatToastType.Danger, "My Profile Error");
                        }
                    }
                    catch (Exception e)
                    {
                        Toaster.Add(HelperService.ConnectionIsUnreachable(), MatToastType.Danger, "My Profile Error");
                        Console.WriteLine(e);
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