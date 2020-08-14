using System;

namespace ManagerAPI.Shared.DTOs
{
    /// <summary>
    /// News list DTO
    /// </summary>
    public class NewsListDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Creator { get; set; }
        public DateTime Creation { get; set; }
        public string LastUpdater { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}