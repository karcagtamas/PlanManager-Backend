namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// My series list DTO
    /// </summary>
    public class MySeriesListDto : SeriesListDto
    {
        public bool IsMine { get; set; }
        public bool IsAdded { get; set; }
    }
}