using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.SL
{
    public class Season : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int SeriesId { get; set; }

        public virtual Series Series { get; set; }

        public virtual ICollection<Episode> Episodes { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && this.Id == ((Season)obj).Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Number, SeriesId, Series, Episodes);
        }

        public override string ToString()
        {
            return $"{this.Id} - {this.Number}";
        }
    }
}
