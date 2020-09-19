using EventManager.Client.Models;
using ManagerAPI.Shared;
using ManagerAPI.Shared.Enums;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManager.Client.Shared.Common
{
    public partial class ListTable<TList> where TList : IIdentified
    {
        [Parameter]
        public List<TableHeaderData<TList>> Header { get; set; }

        [Parameter] public List<TList> Body { get; set; }
        [Parameter] public EventCallback<TList> OnRowClick { get; set; }
        [Parameter] public List<int> SelectedIndexes { get; set; } = new List<int>();
        [Parameter] public bool IsSelectionEnabled { get; set; } = false;
        [Parameter] public bool FooterDisplay { get; set; } = true;
        [Parameter] public bool ShowFilter { get; set; } = false;
        private TableHeaderData<TList> OrderBy { get; set; }
        private List<TList> DisplayList { get; set; } = new List<TList>();
        private OrderDirection Direction { get; set; } = OrderDirection.None;
        private string FilterValue { get; set; } = "";

        protected override void OnParametersSet()
        {
            this.DoFilteringAndOrdering();
        }

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

        private void Filter(string val)
        {
            this.FilterValue = val;
            this.DoFilteringAndOrdering();
        }

        private void HeaderClick(TableHeaderData<TList> data)
        {
            if (data.IsSortable)
            {
                if (this.OrderBy == null || this.OrderBy.PropertyName != data.PropertyName)
                {
                    this.OrderBy = data;
                    this.Direction = OrderDirection.Ascend;
                }
                else
                {
                    if (this.OrderBy.PropertyName == data.PropertyName)
                    {
                        switch (this.Direction)
                        {
                            case OrderDirection.Ascend:
                                this.Direction = OrderDirection.Descend;
                                break;

                            case OrderDirection.Descend:
                                this.Direction = OrderDirection.None;
                                this.OrderBy = null;
                                break;
                        }
                    }
                }

                Console.WriteLine(OrderDirectionService.GetValue(this.Direction));
                this.DoFilteringAndOrdering();
            }
        }

        private void DoFilteringAndOrdering()
        {
            var query = this.Body.AsQueryable();

            if (!string.IsNullOrEmpty(this.FilterValue))
            {
                query = query.Where(x => IsFiltered(x));
            }

            if (this.OrderBy != null)
            {
                query = this.Direction switch
                {
                    OrderDirection.Ascend => query.OrderBy(x => this.GetProperty(x, this.OrderBy.PropertyName)),
                    OrderDirection.Descend => query.OrderByDescending(x => this.GetProperty(x, this.OrderBy.PropertyName)),
                    _ => query
                };
            }

            this.DisplayList = query.ToList();
            StateHasChanged();
        }

        private bool IsFiltered(TList val)
        {
            var res = false;

            foreach (var head in this.Header)
            {
                if (head.IsFilterable && !res)
                {
                    var propVal = head.Displaying(this.GetProperty(val, head.PropertyName));
                    res = propVal.ToLower().Contains(this.FilterValue.ToLower());
                }
            }

            return res;
        }
    }
}