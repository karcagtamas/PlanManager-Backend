using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models.Events
{
    public class SportEventUpdateDto
    {
        [Required] 
        public int Id { get; set; }
        
        public int? Injured { get; set; }

        public int? Visitors { get; set; }

        public int? VisitorLimit { get; set; }
        
        public decimal? VisitorCost { get; set; }

        public int? Players { get; set; }

        public int? PlayerLimit { get; set; }
        
        public decimal? PlayerCost { get; set; }
        
        public decimal? PlayerDeposit { get; set; }

        public int? Helpers { get; set; }

        public int? HelperLimit { get; set; }
        
        public decimal? FixTeamDeposit { get; set; }
        
        public decimal? FixTeamCost { get; set; }

        public int? TeamLimit { get; set; }

        public string MatchJudges { get; set; }
        
        public string Doctors { get; set; }
        
        public SportEventUpdateDto() {}

        public SportEventUpdateDto(SportEventDto sportEvent)
        {
            this.Id = sportEvent.Id;
            this.Injured = sportEvent.Injured;
            this.Visitors = sportEvent.Visitors;
            this.VisitorLimit = sportEvent.VisitorLimit;
            this.VisitorCost = sportEvent.VisitorCost;
            this.Players = sportEvent.Players;
            this.PlayerLimit = sportEvent.PlayerLimit;
            this.PlayerCost = sportEvent.PlayerCost;
            this.PlayerDeposit = sportEvent.PlayerDeposit;
            this.Helpers = sportEvent.Helpers;
            this.HelperLimit = sportEvent.HelperLimit;
            this.FixTeamDeposit = sportEvent.FixTeamDeposit;
            this.FixTeamCost = sportEvent.FixTeamCost;
            this.TeamLimit = sportEvent.TeamLimit;
            this.MatchJudges = sportEvent.MatchJudges;
            this.Doctors = sportEvent.Doctors;
        }
    }
}