using System;

namespace ManagerAPI.Shared.DTOs.SL
{
    public class MovieCommentListDto
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string UserId { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Comment { get; set; }
        public bool OwnerIsCurrent { get; set; }
    }
}