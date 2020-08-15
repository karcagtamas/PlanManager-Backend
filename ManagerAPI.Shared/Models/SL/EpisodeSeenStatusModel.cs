using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// Episode seen status model
    /// </summary>
    public class EpisodeSeenStatusModel
    {
        [Required] public int Id { get; set; }

        [Required] public bool Seen { get; set; }
    }
}