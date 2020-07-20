using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models.WM
{
    public class WorkingFieldDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
    }
}