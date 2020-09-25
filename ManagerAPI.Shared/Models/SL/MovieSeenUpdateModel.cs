using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// Movie seen update model
    /// </summary>
    public class MovieSeenUpdateModel
    {
        [Required] public int Id { get; set; }

        [Required] public bool Seen { get; set; }
    }
}