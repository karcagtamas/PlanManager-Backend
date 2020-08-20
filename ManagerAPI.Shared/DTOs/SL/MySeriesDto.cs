using System;
using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// My series DTO
    /// </summary>
    public class MySeriesDto : SeriesDto
    {
        public bool IsMine { get; set; }
        public bool IsSeen { get; set; }
        public DateTime? AddedOn { get; set; }
        public int Rate { get; set; }
        public List<MySeasonDto> Seasons { get; set; }
    }
}