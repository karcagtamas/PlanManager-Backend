using System;

namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// My movie DTO
    /// </summary>
    public class MyMovieDto : MovieDto
    {
        public bool IsMine { get; set; }
        public bool IsSeen { get; set; }
        public DateTime? SeenOn { get; set; }
        public DateTime? AddedOn { get; set; }
        public int Rate { get; set; }
    }
}