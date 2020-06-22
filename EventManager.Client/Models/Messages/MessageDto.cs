namespace EventManager.Client.Models.Messages
{
    public class MessageDto
    {
        public string Text { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public bool IsMine { get; set; }

        public DateTime Date { get; set; }
    }
}