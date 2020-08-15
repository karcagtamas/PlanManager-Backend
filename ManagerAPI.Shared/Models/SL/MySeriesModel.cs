using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// My series update model
    /// </summary>
    public class MySeriesModel
    {
        [Required] public List<int> Ids { get; set; }
    }
}