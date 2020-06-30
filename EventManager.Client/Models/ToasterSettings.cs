namespace EventManager.Client.Models
{
    public class ToasterSettings
    {
        public string Caption { get; set; }

        public bool IsNeeded { get; set; }

        
        public ToasterSettings() {
            this.IsNeeded = false;
        }

        public ToasterSettings(string caption) {
            this.Caption = caption;
            this.IsNeeded = true;
        }
    }
}