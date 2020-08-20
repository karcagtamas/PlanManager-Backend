using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ManagerAPI.Shared.Annotations;

namespace ManagerAPI.Domain.Entities.SL
{
    public class Series : IEntity
    {
        [Required] public int Id { get; set; }
        [Required] [MaxLength(150)] public string Title { get; set; }
        [MaxLength(999)] public string Description { get; set; }
        [MinNumber(1900)] public int? StartYear { get; set; }
        [MinNumber(1900)] public int? EndYear { get; set; }
        [MaxLength(200)] public string TrailerUrl { get; set; }
        [MaxLength(100)] public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        [Required] public DateTime Creation { get; set; }
        [Required] public DateTime LastUpdate { get; set; }
        [Required] public string CreatorId { get; set; }
        [Required] public string LastUpdaterId { get; set; }
        public virtual ICollection<Season> Seasons { get; set; }
        public virtual User Creator { get; set; }
        public virtual User LastUpdater { get; set; }
        public virtual ICollection<UserSeries> ConnectedUsers { get; set; }
        public virtual ICollection<SeriesSeriesCategory> Categories { get; set; }
        public virtual ICollection<SeriesComment> Comments { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && this.Id == ((Series) obj).Id;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Title);
            hash.Add(Description);
            hash.Add(TrailerUrl);
            hash.Add(ImageTitle);
            hash.Add(ImageData);
            hash.Add(StartYear);
            hash.Add(EndYear);
            hash.Add(Creation);
            hash.Add(LastUpdate);
            hash.Add(CreatorId);
            hash.Add(LastUpdaterId);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return EntityStringBuilder.BuildString(this, "Id", "Title");
        }
    }
}