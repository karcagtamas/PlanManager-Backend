using EventManager.Client.Models;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.Models.SL;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EventManager.Client.Shared.Components.SL
{
    public partial class SeriesImageDialog
    {
        [CascadingParameter] public ModalParameters Parameters { get; set; }

        [CascadingParameter] public BlazoredModal BlazoredModal { get; set; }

        [Inject] private ISeriesService SeriesService { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        [Inject] private IMatToaster Toaster { get; set; }

        private int FormId { get; set; }
        private int Id { get; set; }
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
            FormId = Parameters.Get<int>("FormId");
            Id = Parameters.TryGet<int>("series");

            ((ModalService)ModalService).OnConfirm += OnConfirm;
        }

        private async void OnConfirm()
        {
            if (this.File == null)
            {
                return;
            }

            if (ImageExtensions.Contains(File.Type))
            {
                try
                {
                    await using var stream = new MemoryStream();
                    await this.File.WriteToStreamAsync(stream);

                    if (!await this.SeriesService.UpdateImage(this.Id,
                        new SeriesImageModel { ImageData = stream.ToArray(), ImageTitle = this.File.Name })) return;
                    ModalService.Close(ModalResult.Ok(true));
                    ((ModalService)ModalService).OnConfirm -= OnConfirm;
                }
                catch (Exception e)
                {
                    Toaster.Add("Problem during the image uploading. Please try again later.", MatToastType.Danger,
                        "Series Update Error");
                    Console.WriteLine(e);
                }
            }
            else
            {
                Toaster.Add("Invalid file extension. Please try again with a correct type.", MatToastType.Danger,
                    "Series Update Error");
            }
        }

        private void FilesReady(IMatFileUploadEntry[] files)
        {
            this.File = files.FirstOrDefault();
        }
    }
}