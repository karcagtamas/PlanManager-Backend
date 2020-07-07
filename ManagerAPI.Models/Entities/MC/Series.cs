using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities.MC
{
    public class Series
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public int? StartYear { get; set; }

        public int? EndYear { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        public string CreaterId { get; set; }

        public virtual ICollection<Season> Seasons { get; set; }
        public virtual User Creater { get; set; }
    }
}
