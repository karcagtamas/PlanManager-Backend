using System;
using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// Movie DTO
    /// </summary>
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ReleaseYear { get; set; }
        public int? Length { get; set; }
        public string Director { get; set; }
        public string TrailerUrl { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string Creator { get; set; }
        public string LastUpdater { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<string> Categories { get; set; }
        public int NumberOfSeen { get; set; }
    }
}