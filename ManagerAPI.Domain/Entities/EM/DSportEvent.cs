using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerAPI.Domain.Entities.EM
{
    public class DSportEvent
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        public int? Injured { get; set; }

        public int? Visitors { get; set; }

        public int? VisitorLimit { get; set; }
        
        [Column(TypeName = "decimal(10,4)")]
        public decimal? VisitorCost { get; set; }

        public int? Players { get; set; }

        public int? PlayerLimit { get; set; }
        
        [Column(TypeName = "decimal(10,4)")]
        public decimal? PlayerCost { get; set; }
        
        [Column(TypeName = "decimal(10,4)")]
        public decimal? PlayerDeposit { get; set; }

        public int? Helpers { get; set; }

        public int? HelperLimit { get; set; }
        
        [Column(TypeName = "decimal(10,4)")]
        public decimal? FixTeamDeposit { get; set; }
        
        [Column(TypeName = "decimal(10,4)")]
        public decimal? FixTeamCost { get; set; }

        public int? TeamLimit { get; set; }

        public string MatchJudges { get; set; }
        
        public string Doctors { get; set; }
        
        public virtual MasterEvent Event { get; set; }
    }
}