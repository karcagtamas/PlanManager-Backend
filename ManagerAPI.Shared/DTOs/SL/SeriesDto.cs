using System;
using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// Series DTO
    /// </summary>
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
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string TrailerUrl { get; set; }
        public List<string> Categories { get; set; }
    }
}