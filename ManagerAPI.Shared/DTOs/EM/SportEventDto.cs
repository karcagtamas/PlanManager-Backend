namespace ManagerAPI.Shared.DTOs.EM
{
    /// <summary>
    /// Sport event DTO
    /// </summary>
    public class SportEventDto
    {
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
    }
}