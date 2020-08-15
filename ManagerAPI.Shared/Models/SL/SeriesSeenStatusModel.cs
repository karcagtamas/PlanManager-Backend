using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// Series seen status model
    /// </summary>
    public class SeriesSeenStatusModel
    {
        [Required] public int Id { get; set; }

        [Required] public bool Seen { get; set; }
    }
}