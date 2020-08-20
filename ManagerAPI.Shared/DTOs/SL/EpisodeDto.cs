namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// Episode DTO
    /// </summary>
    public class EpisodeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int Number { get; set; }
        public string Description { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string LastUpdater { get; set; }
    }
}