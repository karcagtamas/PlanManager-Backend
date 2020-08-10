using System;
using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.MC
{
    public class MySeriesListDto : SeriesListDto
    {
        public bool IsMine { get; set; }
    }
}