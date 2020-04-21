using System;
using System.ComponentModel.DataAnnotations;

namespace PlanManager.Services.DTOs.EM
{
    public class MyEventListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Members { get; set; }
        public string Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string Type { get; set; }
    }

    public class MasterEventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsPublic { get; set; }
        public string CreatorId { get; set; }
        public string Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastUpdaterId { get; set; }
        public string LastUpdater { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StartHour { get; set; }
        public int? EndHour { get; set; }
        public int? StartMinute { get; set; }
        public int? EndMinute { get; set; }
    }

    public class EventDto
    {
        public MasterEventDto MasterEvent { get; set; }
        public SportEventDto SportEvent { get; set; }
        public GtEventDto GtEvent { get; set; }
    }

    public class GtEventDto
    {
        public int Id { get; set; }
        public string TShirtColor { get; set; }
        public int? Greeny { get; set; }
        public decimal? GreenyCost { get; set; }
    }

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

    }

    public class EventCreateDto
    {
        [Required]
        public string Title { get; set; }
        
        public string Description { get; set; }
    }

    public class MasterEventUpdateDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? StartHour { get; set; }

        public int? EndHour { get; set; }

        public int? StartMinute { get; set; }

        public int? EndMinute { get; set; }
    }

    public class GtEventUpdateDto
    {
        [Required]
        public int Id { get; set; }
        
        public string TShirtColor { get; set; }

        public int? Greeny { get; set; }
        
        public decimal? GreenyCost { get; set; }
    }

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
    }
        
}