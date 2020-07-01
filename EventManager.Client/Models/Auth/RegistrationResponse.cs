namespace EventManager.Client.Models.Auth
{
    public class RegistrationResponse
    {
        public bool Succeeded { get; set; }
        public RegistrationError[] Errors { get; set; }
    }
}