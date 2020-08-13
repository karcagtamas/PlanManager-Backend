namespace ManagerAPI.Shared.DTOs.MC
{
    public class MyEpisodeDto : EpisodeDto
    {
        public bool IsMine { get; set; }
        public bool IsSeen { get; set; }
    }
}