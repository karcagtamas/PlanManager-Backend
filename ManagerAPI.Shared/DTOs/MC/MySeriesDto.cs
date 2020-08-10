using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.MC
{
    public class MySeriesDto : SeriesDto
    {
        public bool IsMine { get; set; }
        public List<MySeasonDto> Seasons { get; set; }
    }
}