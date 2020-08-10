using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.DTOs.MC
{
    public class SeriesDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public string Creator { get; set; }
        public string LastUpdater { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
