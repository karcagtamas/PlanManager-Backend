namespace ManagerAPI.Shared.DTOs.MC
{
    /// <summary>
    /// My series list DTO
    /// </summary>
    public class MySeriesListDto : SeriesListDto
    {
        public bool IsMine { get; set; }
    }
}