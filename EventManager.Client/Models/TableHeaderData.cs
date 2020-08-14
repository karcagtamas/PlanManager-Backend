﻿using System;

namespace EventManager.Client.Models
{
    public class TableHeaderData
    {
        public string PropertyName { get; set; }
        public string DisplayName { get; set; }
        public Func<object, string> Displaying { get; set; }
        public bool IsSortable { get; set; }
    }
}