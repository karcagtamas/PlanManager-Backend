using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.SL
{
    public class SeriesComment : IEntity
    {
        public int Id { get; set; }
        [Required] public int SeriesId { get; set; }
        [Required] public string UserId { get; set; }
        [Required] public DateTime Creation { get; set; }
        [Required] public DateTime LastUpdate { get; set; }
        [Required] [MaxLength(500)] public string Comment { get; set; }
        public virtual Series Series { get; set; }
        public virtual User User { get; set; }
    }
}