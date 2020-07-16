using System;

namespace ManagerAPI.Models.DTOs.MC
{
    public class BookListDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public DateTime? Publish { get; set; }

        public string Creator { get; set; }
    }
}