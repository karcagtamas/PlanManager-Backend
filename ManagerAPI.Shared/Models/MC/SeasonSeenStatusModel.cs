using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.MC
{
    /// <summary>
    /// Season seen status model
    /// </summary>
    public class SeasonSeenStatusModel
    {
        [Required] public int Id { get; set; }

        [Required] public bool Seen { get; set; }
    }
}