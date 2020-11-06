using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerAPI.Domain.Entities

{
    public class Task : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsSolved { get; set; } = false;

        [Required]
        public string OwnerId { get; set; }

        [Required]
        public DateTime Creation { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Deadline { get; set; }

        public virtual User Owner { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && this.Id == ((Task)obj).Id;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(this.Id);
            hash.Add(this.Title);
            hash.Add(this.Description);
            hash.Add(this.IsSolved);
            hash.Add(this.OwnerId);
            hash.Add(this.Creation);
            hash.Add(this.LastUpdate);
            hash.Add(this.Deadline);
            hash.Add(this.Owner);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return $"{this.Id} - {this.Title}";
        }
    }
}
