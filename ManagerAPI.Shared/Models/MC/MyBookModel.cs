using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.MC
{
    /// <summary>
    /// My book update model
    /// </summary>
    public class MyBookModel
    {
        [Required] public List<int> Ids { get; set; }
    }
}