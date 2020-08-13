using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.MC
{
    /// <summary>
    /// My movie update model
    /// </summary>
    public class MyMovieModel
    {
        [Required] public List<int> Ids { get; set; }
    }
}