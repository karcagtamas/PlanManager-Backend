﻿using System;

namespace ManagerAPI.Shared.DTOs.CSM
{
    public class WorkTable
    {
        public int? Id;
        public DateTime Date;
        public bool IsActive { get; set; }
        public string PersonId { get; set; }
    }
}