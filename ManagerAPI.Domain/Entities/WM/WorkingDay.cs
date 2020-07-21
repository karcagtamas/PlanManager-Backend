using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerAPI.Domain.Entities.WM
{
    public class WorkingDay {

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Day { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int TypeId { get; set; }

        public virtual WorkingDayType Type { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<WorkingField> WorkingFields { get; set; }
    }
}
