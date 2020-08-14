using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.MC
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