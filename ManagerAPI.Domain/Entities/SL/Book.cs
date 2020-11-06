using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.SL
{
    public class Book : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Author { get; set; }

        public string Description { get; set; }

        public DateTime? Publish { get; set; }

        [Required]
        public string CreatorId { get; set; }

        [Required]
        public string LastUpdaterId { get; set; }

        [Required]
        public DateTime Creation { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }

        [Required]
        public virtual User Creator { get; set; }

        [Required]
        public virtual User LastUpdater { get; set; }

        public virtual ICollection<UserBook> ConnectedUsers { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && this.Id == ((Book)obj).Id;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(this.Id);
            hash.Add(this.Name);
            hash.Add(this.Author);
            hash.Add(this.Description);
            hash.Add(this.Publish);
            hash.Add(this.CreatorId);
            hash.Add(this.LastUpdaterId);
            hash.Add(this.Creation);
            hash.Add(this.LastUpdate);
            hash.Add(this.Creator);
            hash.Add(this.LastUpdater);
            hash.Add(this.ConnectedUsers);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return $"{this.Id} - {this.Name}";
        }
    }
}