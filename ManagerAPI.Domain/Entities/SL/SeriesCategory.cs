using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.SL
{
    public class SeriesCategory : IEntity
    {
        [Required] public int Id { get; set; }

        [Required] [MaxLength(120)] public string Name { get; set; }
        public virtual ICollection<SeriesSeriesCategory> ConnectedSeriesList { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && this.Id == ((Movie)obj).Id;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(this.Id);
            hash.Add(this.Name);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return EntityStringBuilder.BuildString(this, "Id", "Name");
        }
    }
}