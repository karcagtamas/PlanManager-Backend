namespace ManagerAPI.Models.Entities.MC
{
    public class UserEpisode
    {
        public string UserId { get; set; }

        public int EpisodeId { get; set; }

        public bool Seen { get; set; }

        public virtual User User { get; set; }

        public virtual Episode Episode { get; set; }
    }
}
