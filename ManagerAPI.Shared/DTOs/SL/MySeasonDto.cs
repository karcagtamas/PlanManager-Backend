using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// My season DTO
    /// </summary>
    public class MySeasonDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool IsSeen { get; set; }
        public List<MyEpisodeListDto> Episodes { get; set; }
    }
}