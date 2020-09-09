using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.CSM
{
    public class CsomorWork
    {
        [Required] public string Id { get; set; }

        [Required] [MaxLength(80)] public string Name { get; set; }

        [Required] public int CsomorId { get; set; }

        public virtual Csomor Csomor { get; set; }
        public virtual ICollection<CsomorWorkTable> Tables { get; set; }
        public virtual ICollection<CsomorPersonTable> Persons { get; set; }
        public virtual ICollection<IgnoredWork> IgnoringPersons { get; set; }
    }
}