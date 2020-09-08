using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.CSM
{
    public class CsomorWork
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CsomorId { get; set; }

        public virtual Csomor Csomor { get; set; }
    }
}