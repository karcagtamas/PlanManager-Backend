using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.MC
{
    public class MySeriesDto : SeriesDto
    {
        public bool IsMine { get; set; }
        public bool IsSeen { get; set; } = false;
        public List<MySeasonDto> Seasons { get; set; }
    }
}