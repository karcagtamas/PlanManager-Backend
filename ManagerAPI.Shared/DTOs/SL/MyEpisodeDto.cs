﻿using System;

namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// My episode DTO
    /// </summary>
    public class MyEpisodeDto : EpisodeDto
    {
        public bool IsMine { get; set; }
        public bool IsSeen { get; set; }
        public DateTime? SeenOn { get; set; }
    }
}