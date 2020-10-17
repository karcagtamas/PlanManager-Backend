using ManagerAPI.Shared.DTOs.EM;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.EM
{
    /// <summary>
    /// Sport event create or update model
    /// </summary>
    public class SportEventModel
    {
        [Required] public int Id { get; set; }

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

        /// <summary>
        /// Empty init
        /// </summary>
        public SportEventModel()
        {
        }

        /// <summary>
        /// Model from sport event data object
        /// </summary>
        /// <param name="sportEvent">Sport event data object</param>
        public SportEventModel(SportEventDto sportEvent)
        {
            Id = sportEvent.Id;
            Injured = sportEvent.Injured;
            Visitors = sportEvent.Visitors;
            VisitorLimit = sportEvent.VisitorLimit;
            VisitorCost = sportEvent.VisitorCost;
            Players = sportEvent.Players;
            PlayerLimit = sportEvent.PlayerLimit;
            PlayerCost = sportEvent.PlayerCost;
            PlayerDeposit = sportEvent.PlayerDeposit;
            Helpers = sportEvent.Helpers;
            HelperLimit = sportEvent.HelperLimit;
            FixTeamDeposit = sportEvent.FixTeamDeposit;
            FixTeamCost = sportEvent.FixTeamCost;
            TeamLimit = sportEvent.TeamLimit;
            MatchJudges = sportEvent.MatchJudges;
            Doctors = sportEvent.Doctors;
        }
    }
}