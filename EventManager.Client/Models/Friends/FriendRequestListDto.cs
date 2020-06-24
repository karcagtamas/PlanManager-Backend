using System;

namespace EventManager.Client.Models.Friends
{
    public class FriendRequestListDto
    {
        public int Id { get; set; }

        public string Sender { get; set; }

        public DateTime SentDate { get; set; }

        public string Message { get; set; }

        public bool? Response { get; set; }

        public DateTime? ResponseDate { get; set; }
    }
}