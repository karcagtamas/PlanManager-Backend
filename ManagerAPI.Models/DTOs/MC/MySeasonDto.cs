using System.Collections.Generic;

namespace ManagerAPI.Models.DTOs.MC
{
    public class MySeasonDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public List<MyEpisodeDto> Episodes { get; set; }
    }
}