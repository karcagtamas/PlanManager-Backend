using System;
using System.Collections.Generic;
using System.Linq;
using EventManager.Client.Models;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Common
{
    public partial class ListTable<TList>
    {
        [Parameter]
        public List<TableHeaderData> Header { get; set; }
        
        [Parameter]
        public List<string> Footer { get; set; }
        
        [Parameter]
        public List<TList> Body { get; set; }
        
        public List<int> IndexList { get; set; } = new List<int>();

        protected override void OnInitialized()
        {
            for (int i = 0; i < Header.Count; i++)
            {
                IndexList.Add(i);
            }
        }
    }
}