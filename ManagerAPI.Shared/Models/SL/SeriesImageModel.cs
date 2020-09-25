using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// Series image update model
    /// </summary>
    public class SeriesImageModel
    {
        [MaxLength(100)] public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}