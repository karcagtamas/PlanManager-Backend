using ManagerAPI.Shared.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.CSM
{
    public class Csomor
    {
        [Required] public int Id { get; set; }
        [Required] [MaxLength(120)] public string Title { get; set; }
        [Required] public DateTime Creation { get; set; }
        [Required] public string OwnerId { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string LastUpdaterId { get; set; }
        [Required] public DateTime Start { get; set; }
        [Required] public DateTime Finish { get; set; }

        [Required]
        [MinNumber(1)]
        [MaxNumber(8)]
        public int MaxWorkHour { get; set; }

        [Required]
        [MinNumber(1)]
        [MaxNumber(4)]
        public int MinRestHour { get; set; }

        [Required] public bool IsShared { get; set; }
        [Required] public bool IsPublic { get; set; }
        [Required] public bool HasGeneratedCsomor { get; set; }
        public DateTime? LastGeneration { get; set; }

        public virtual User Owner { get; set; }
        public virtual User LastUpdater { get; set; }
        public virtual ICollection<CsomorPerson> Persons { get; set; }
        public virtual ICollection<CsomorWork> Works { get; set; }
        public virtual ICollection<UserCsomor> SharedWith { get; set; }
    }
}