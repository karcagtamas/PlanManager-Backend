using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// Movie image update model
    /// </summary>
    public class MovieImageModel
    {
        [MaxLength(100)] public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}