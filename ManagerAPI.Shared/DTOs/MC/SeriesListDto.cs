﻿using System;
using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.MC
{
    public class SeriesListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public DateTime Creation { get; set; }
        public string Creator { get; set; }
    }
}
