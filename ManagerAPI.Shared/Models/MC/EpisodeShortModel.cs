using ManagerAPI.Shared.DTOs.MC;

namespace ManagerAPI.Shared.Models.MC
{
    public class EpisodeShortModel
    {
        public string Description { get; set; }
        
        public EpisodeShortModel() {}

        public EpisodeShortModel(EpisodeDto episode)
        {
            this.Description = episode.Description;
        }
    }
}