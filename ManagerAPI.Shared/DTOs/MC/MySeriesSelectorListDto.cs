namespace ManagerAPI.Shared.DTOs.MC
{
    /// <summary>
    /// My series selector list DTO
    /// </summary>
    public class MySeriesSelectorListDto : SeriesListDto
    {
        public bool IsMine { get; set; }
    }
}