using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.SL
{
    public class UserMovie
    {
        [Required] public int MovieId { get; set; }
        [Required] public string UserId { get; set; }
        [Required] public bool IsSeen { get; set; }
        [Required] public bool IsAdded { get; set; }
        public DateTime? SeenOn { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? Rate { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}