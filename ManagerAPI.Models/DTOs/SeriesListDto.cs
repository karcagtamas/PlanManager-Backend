using System;
using System.Collections.Generic;

namespace ManagerAPI.Models.DTOs
{
    public class SeriesListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public DateTime CreationTime { get; set; }
        public List<SeasonListDto> Seasons { get; set; }
        public string Creater { get; set; }
    }
}
