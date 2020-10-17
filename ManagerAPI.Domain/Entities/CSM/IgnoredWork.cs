using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.CSM
{
    public class IgnoredWork
    {
        [Required]
        public string PersonId { get; set; }

        [Required]
        public string WorkId { get; set; }

        public virtual CsomorPerson Person { get; set; }
        public virtual CsomorWork Work { get; set; }
    }
}