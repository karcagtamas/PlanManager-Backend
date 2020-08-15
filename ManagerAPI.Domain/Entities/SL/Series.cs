using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.SL
{
    public class Series : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int? StartYear { get; set; }

        public int? EndYear { get; set; }

        [Required]
        public DateTime Creation { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }

        [Required]
        public string CreatorId { get; set; }

        [Required]
        public string LastUpdaterId { get; set; }

        public virtual ICollection<Season> Seasons { get; set; }
        public virtual User Creator { get; set; }
        public virtual User LastUpdater { get; set; }
        public virtual ICollection<UserSeries> ConnectedUsers { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && this.Id == ((Book)obj).Id;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Title);
            hash.Add(Description);
            hash.Add(StartYear);
            hash.Add(EndYear);
            hash.Add(Creation);
            hash.Add(LastUpdate);
            hash.Add(CreatorId);
            hash.Add(LastUpdaterId);
            hash.Add(Seasons);
            hash.Add(Creator);
            hash.Add(LastUpdater);
            hash.Add(ConnectedUsers);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return $"{this.Id} - {this.Title}";
        }
    }
}
