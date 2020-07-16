using System;

namespace ManagerAPI.Models.DTOs.MC
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime? Publish { get; set; }
        public string Creator { get; set; }
        public string LastUpdater { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}