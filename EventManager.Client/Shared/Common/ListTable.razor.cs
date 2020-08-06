using System.Collections.Generic;
using EventManager.Client.Models;
using ManagerAPI.Shared;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Common
{
    public partial class ListTable<TList> where TList : IIdentified
    {
        [Parameter] public List<TableHeaderData> Header { get; set; }
        [Parameter] public List<string> Footer { get; set; }
        [Parameter] public List<TList> Body { get; set; }
        [Parameter] public EventCallback<TList> OnRowClick { get; set; }
        [Parameter] public List<int> SelectedIndexes { get; set; } = new List<int>();
        [Parameter] public bool IsSelectionEnabled { get; set; }
        

        private object GetProperty(TList entity, string property)
        {
            var type = typeof(TList);
            var prop = type.GetProperty(property);
            if (prop != null) return prop.GetValue(entity);

            return "";
        }

        private void RowClick(TList entity)
        {
            OnRowClick.InvokeAsync(entity);
        }
    }
}