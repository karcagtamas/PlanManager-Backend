namespace ManagerAPI.Shared.DTOs.MC
{
    /// <summary>
    /// My episode DTO
    /// </summary>
    public class MyEpisodeDto : EpisodeDto
    {
        public bool IsMine { get; set; }
        public bool IsSeen { get; set; }
    }
}