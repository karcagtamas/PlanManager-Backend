using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// Episode create or update model
    /// </summary>
    public class EpisodeModel
    {
        [Required] public int Number { get; set; }

        public string Description { get; set; }

        [Required] public int SeasonId { get; set; }
    }
}