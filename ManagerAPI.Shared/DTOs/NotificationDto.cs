using System;

namespace ManagerAPI.Shared.DTOs
{
    /// <summary>
    /// Notification DTO
    /// </summary>
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }
        public bool Archived { get; set; }
        public string TypeTitle { get; set; }
        public int ImportanceLevel { get; set; }
        public string SystemName { get; set; }
        public string SystemShortName { get; set; }
    }
}