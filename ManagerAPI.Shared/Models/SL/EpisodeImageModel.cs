using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// Episode image update model
    /// </summary>
    public class EpisodeImageModel
    {
        
        [MaxLength(100)] public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}