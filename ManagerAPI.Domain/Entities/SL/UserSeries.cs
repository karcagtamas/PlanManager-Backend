using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.SL
{
    public class UserSeries
    {
        [Required] public int SeriesId { get; set; }
        [Required] public string UserId { get; set; }
        [Required] public bool IsAdded { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? Rate { get; set; }
        public virtual Series Series { get; set; }
        public virtual User User { get; set; }
    }
}