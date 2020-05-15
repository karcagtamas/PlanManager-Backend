using System;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models.Events
{
    public class MasterEventUpdateDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }

        public string Description { get; set; }
        
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        
        [Required]
        public bool IsLocked { get; set; }
        
        [Required]
        public bool IsDisabled { get; set; }
        
        [Required]
        public bool IsPublic { get; set; }
        
        public MasterEventUpdateDto() {}

        public MasterEventUpdateDto(MasterEventDto masterEvent)
        {
            this.Id = masterEvent.Id;
            this.Title = masterEvent.Title;
            this.Description = masterEvent.Description;
            this.StartDate = masterEvent.StartDate;
            this.EndDate = masterEvent.EndDate;
            this.IsLocked = masterEvent.IsLocked;
            this.IsDisabled = masterEvent.IsDisabled;
            this.IsPublic = masterEvent.IsPublic;
        }
    }
}