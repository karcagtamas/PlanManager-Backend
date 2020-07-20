using System;

namespace ManagerAPI.Shared.DTOs.MC
{
    public class MovieDto
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public string Creator { get; set; }

        public string LastUpdater { get; set; }

        public DateTime Creation { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}