using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerAPI.Domain.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(400)")]
        public string Text { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual User Sender { get; set; }

        public virtual User Receiver { get; set; }

    }
}
