using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.MC
{
    public class MySeasonDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool IsSeen { get; set; }
        public List<MyEpisodeDto> Episodes { get; set; }
    }
}