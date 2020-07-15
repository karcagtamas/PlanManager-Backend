using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities.MC
{
    public class Series
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
    }
}
