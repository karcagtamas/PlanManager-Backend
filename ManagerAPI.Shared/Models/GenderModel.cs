using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models
{
    /// <summary>
    /// Gender create or update model
    /// </summary>
    public class GenderModel
    {
        [Required(ErrorMessage = "Field is required")]
        public string Name { get; set; }
    }
}