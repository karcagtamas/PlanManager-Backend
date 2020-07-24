using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerAPI.Domain.Entities.WM
{
    public class WorkingDay : IEntity
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Day { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int TypeId { get; set; }

        public virtual WorkingDayType Type { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<WorkingField> WorkingFields { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && this.Id == ((WorkingDay)obj).Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Day, UserId, TypeId);
        }

        public override string ToString()
        {
            return $"{this.Id} - {this.Day}";
        }
    }
}
