using ManagerAPI.Shared.DTOs.MC;

namespace ManagerAPI.Shared.Models.MC
{
    /// <summary>
    /// Episode update model
    /// </summary>
    public class EpisodeShortModel
    {
        public string Description { get; set; }

        /// <summary>
        /// Empty init
        /// </summary>
        public EpisodeShortModel()
        {
        }

        /// <summary>
        /// Model from episode data object
        /// </summary>
        /// <param name="episode"></param>
        public EpisodeShortModel(EpisodeDto episode)
        {
            this.Description = episode.Description;
        }
    }
}