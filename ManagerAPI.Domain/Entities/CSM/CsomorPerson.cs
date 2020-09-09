using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.CSM
{
    public class CsomorPerson
    {
        [Required] public string Id { get; set; }

        [Required] [MaxLength(120)] public string Name { get; set; }

        [Required] public int CsomorId { get; set; }

        [Required] public int PlusWorkCounter { get; set; }
        [Required] public bool IsIgnored { get; set; }

        public virtual Csomor Csomor { get; set; }
        public virtual ICollection<CsomorPersonTable> Tables { get; set; }
        public virtual ICollection<CsomorWorkTable> Works { get; set; }
        public virtual ICollection<IgnoredWork> IgnoredWorks { get; set; }
    }
}