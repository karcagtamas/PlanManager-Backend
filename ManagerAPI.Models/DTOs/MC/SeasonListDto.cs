using System.Collections.Generic;

namespace ManagerAPI.Models.DTOs.MC
{
    public class SeasonListDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public virtual List<EpisodeListDto> Episodes { get; set; }
    }
}
