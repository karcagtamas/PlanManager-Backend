using System;

namespace EventManager.Client.Models
{
    public class TableHeaderData
    {
        public string PropertyName { get; set; }
        public string DisplayName { get; set; }
        public Func<object, string> Displaying { get; set; }
        public bool IsSortable { get; set; }
        public bool IsFilterable { get; set; }

        public TableHeaderData(string propertyName)
        {
            this.PropertyName = propertyName;
            this.DisplayName = propertyName;
            this.Displaying = x => (string)x;
            this.IsSortable = false;
            this.IsFilterable = true;
        }
        
        public TableHeaderData(string propertyName, string displayName, bool isSortable, bool isFilterable)
        {
            this.PropertyName = propertyName;
            this.DisplayName = displayName;
            this.Displaying = x => (string)x;
            this.IsSortable = isSortable;
            this.IsFilterable = isFilterable;
        }

        public TableHeaderData(string propertyName, string displayName)
        {
            this.PropertyName = propertyName;
            this.DisplayName = displayName;
            this.Displaying = x => (string)x;
            this.IsSortable = false;
            this.IsFilterable = true;
        }

        public TableHeaderData(string propertyName, string displayName, Func<object, string> displaying)
        {
            this.PropertyName = propertyName;
            this.DisplayName = displayName;
            this.Displaying = displaying;
            this.IsSortable = false;
            this.IsFilterable = true;
        }
        
        public TableHeaderData(string propertyName, string displayName, Func<object, string> displaying, bool isSortable, bool isFilterable)
        {
            this.PropertyName = propertyName;
            this.DisplayName = displayName;
            this.Displaying = displaying;
            this.IsSortable = isSortable;
            this.IsFilterable = isFilterable;
        }
    }
}