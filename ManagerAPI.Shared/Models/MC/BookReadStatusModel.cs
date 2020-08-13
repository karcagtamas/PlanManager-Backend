using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.MC
{
    /// <summary>
    /// Book read status model
    /// </summary>
    public class BookReadStatusModel
    {
        [Required] public int Id { get; set; }

        [Required] public bool Read { get; set; }
    }
}