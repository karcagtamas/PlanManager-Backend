using System.Collections.Generic;
using EventManager.Client.Models;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Common
{
    public partial class ListTable<TList>
    {
        [Parameter] public List<TableHeaderData> Header { get; set; }

        [Parameter] public List<string> Footer { get; set; }

        [Parameter] public List<TList> Body { get; set; }

        [Parameter] public EventCallback<TList> OnRowClick { get; set; }

        private List<int> IndexList { get; } = new List<int>();
        private List<int> BodyIndexList { get; } = new List<int>();

        protected override void OnInitialized()
        {
            for (var i = 0; i < Header.Count; i++) IndexList.Add(i);

            for (var i = 0; i < Body.Count; i++) BodyIndexList.Add(i);
        }

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