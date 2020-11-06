using ManagerAPI.Shared.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.SL
{
    public class Movie : IEntity
    {
        [Required] public int Id { get; set; }
        [Required] [MaxLength(150)] public string Title { get; set; }
        [MaxLength(999)] public string Description { get; set; }

        [MinNumber(1900)] public int? ReleaseYear { get; set; }

        [MaxNumber(999)] [MinNumber(1)] public int? Length { get; set; }

        [MaxLength(60)] public string Director { get; set; }

        [MaxLength(200)] public string TrailerUrl { get; set; }

        [MaxLength(100)] public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        [Required] public string CreatorId { get; set; }
        [Required] public string LastUpdaterId { get; set; }
        [Required] public DateTime Creation { get; set; }
        [Required] public DateTime LastUpdate { get; set; }
        public virtual User Creator { get; set; }
        public virtual User LastUpdater { get; set; }
        public virtual ICollection<UserMovie> ConnectedUsers { get; set; }
        public virtual ICollection<MovieMovieCategory> Categories { get; set; }
        public virtual ICollection<MovieComment> Comments { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && this.Id == ((Movie)obj).Id;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(this.Id);
            hash.Add(this.Title);
            hash.Add(this.Description);
            hash.Add(this.ReleaseYear);
            hash.Add(this.Length);
            hash.Add(this.Director);
            hash.Add(this.TrailerUrl);
            hash.Add(this.ImageTitle);
            hash.Add(this.ImageData);
            hash.Add(this.CreatorId);
            hash.Add(this.LastUpdaterId);
            hash.Add(this.Creation);
            hash.Add(this.LastUpdate);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return EntityStringBuilder.BuildString(this, "Id", "Title", "ReleaseYear");
        }
    }
}