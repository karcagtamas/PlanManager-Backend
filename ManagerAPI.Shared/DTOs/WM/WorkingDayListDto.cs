using System;
using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.WM
{
    /// <summary>
    /// Working day list DTO
    /// </summary>
    public class WorkingDayListDto
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public int Type { get; set; }
        public List<WorkingFieldListDto> WorkingFields { get; set; }
    }
}