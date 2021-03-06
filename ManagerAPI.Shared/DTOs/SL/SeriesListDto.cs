﻿using System;

namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// Series list DTO
    /// </summary>
    public class SeriesListDto : IIdentified
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public DateTime Creation { get; set; }
        public string Creator { get; set; }
    }
}