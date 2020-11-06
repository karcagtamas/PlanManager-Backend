using ManagerAPI.Shared.DTOs.EM;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.EM
{
    /// <summary>
    /// Master event create or update DTO
    /// </summary>
    public class MasterEventModel
    {
        [Required] public int Id { get; set; }
        [Required] [MaxLength(256)] public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required] public bool IsLocked { get; set; }
        [Required] public bool IsDisabled { get; set; }
        [Required] public bool IsPublic { get; set; }

        /// <summary>
        /// Empty init
        /// </summary>
        public MasterEventModel()
        {
        }

        /// <summary>
        /// Model from event data object
        /// </summary>
        /// <param name="masterEvent">Master event data object</param>
        public MasterEventModel(MasterEventDto masterEvent)
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