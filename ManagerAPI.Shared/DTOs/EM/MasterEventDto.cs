using System;

namespace ManagerAPI.Shared.DTOs.EM
{
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
    }
}