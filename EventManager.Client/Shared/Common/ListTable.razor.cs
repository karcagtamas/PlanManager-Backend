using System.Collections.Generic;
using EventManager.Client.Models;
using ManagerAPI.Shared;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Common
{
    public partial class ListTable<TList> where TList : IIdentified
    {
        [Parameter] public List<TableHeaderData> Header { get; set; }
        [Parameter] public List<string> Footer { get; set; } = new List<string>();
        [Parameter] public List<TList> Body { get; set; }
        [Parameter] public EventCallback<TList> OnRowClick { get; set; }
        [Parameter] public List<int> SelectedIndexes { get; set; } = new List<int>();
        [Parameter] public bool IsSelectionEnabled { get; set; } = false;
        [Parameter] public bool FooterDisplay { get; set; } = true;
        [Parameter] public bool ShowFilter { get; set; } = false;
        private TableHeaderData OrderBy { get; set; }
        private string Direction { get; set; } = "none";
        private string FilterValue { get; set; } = "";
        

        private object GetProperty(TList entity, string property)
        {
            var type = typeof(TList);
            var prop = type.GetProperty(property);
            return prop != null ? prop.GetValue(entity) : "";
        }

        private void RowClick(TList entity)
        {
            OnRowClick.InvokeAsync(entity);
        }
        
        private void HeaderClick(TableHeaderData data)
        {
            if (this.OrderBy == null || this.OrderBy.PropertyName != data.PropertyName)
            {
                this.OrderBy = data;
                this.Direction = "asc";
            }

            if (this.OrderBy.PropertyName == data.PropertyName)
            {
                switch (this.Direction)
                {
                    case "asc":
                        this.Direction = "desc";
                        break;
                    case "desc":
                        this.Direction = "none";
                        this.OrderBy = null;
                        break;
                }
            }
            StateHasChanged();
        }
    }
}