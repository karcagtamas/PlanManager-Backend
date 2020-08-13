using System;

namespace ManagerAPI.Shared.DTOs
{
    /// <summary>
    /// Message DTO
    /// </summary>
    public class MessageDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public bool IsMine { get; set; }
        public DateTime Date { get; set; }
    }
}