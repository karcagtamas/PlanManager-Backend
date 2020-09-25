using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ManagerAPI.Shared.Annotations;

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
            return obj != null && ((Episode) obj).Id == this.Id;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Title);
            hash.Add(Description);
            hash.Add(ImageTitle);
            hash.Add(ImageData);
            hash.Add(LastUpdate);
            hash.Add(Number);
            hash.Add(LastUpdaterId);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return $"{this.Id} - {this.Number}";
        }
    }
}