using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// Season create or update model
    /// </summary>
    public class SeasonModel
    {
        [Required] public int Number { get; set; }

        [Required] public int SeriesId { get; set; }
    }
}