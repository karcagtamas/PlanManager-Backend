using ManagerAPI.Shared.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.SL
{
    public class Episode : IEntity
    {
        [Required] public int Id { get; set; }
        [Required] [MaxLength(150)] public string Title { get; set; }

        [Required]
        [MinNumber(1)]
        [MaxNumber(99)]
        public int Number { get; set; }

        [MaxLength(300)] public string Description { get; set; }
        [Required] public int SeasonId { get; set; }
        [Required] public string LastUpdaterId { get; set; }
        [Required] public DateTime LastUpdate { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public virtual Season Season { get; set; }
        public virtual User LastUpdater { get; set; }
        public virtual ICollection<UserEpisode> ConnectedUsers { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && ((Episode)obj).Id == this.Id;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(this.Id);
            hash.Add(this.Title);
            hash.Add(this.Description);
            hash.Add(this.ImageTitle);
            hash.Add(this.ImageData);
            hash.Add(this.LastUpdate);
            hash.Add(this.Number);
            hash.Add(this.LastUpdaterId);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return $"{this.Id} - {this.Number}";
        }
    }
}