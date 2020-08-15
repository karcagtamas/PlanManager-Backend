namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// My series selector list DTO
    /// </summary>
    public class MySeriesSelectorListDto : SeriesListDto
    {
        public bool IsMine { get; set; }
    }
}