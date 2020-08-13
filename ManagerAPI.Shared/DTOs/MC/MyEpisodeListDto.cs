namespace ManagerAPI.Shared.DTOs.MC
{
    public class MyEpisodeListDto : EpisodeListDto
    {
        public string Description { get; set; }
        public bool Seen { get; set; }
    }
}