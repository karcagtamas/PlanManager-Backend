namespace EventManager.Client.Models
{
    public class RegistrationResponse
    {
        public bool Succeeded { get; set; }
        public RegistrationError[] Errors { get; set; }
    }
}