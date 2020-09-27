using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ManagerAPI.Shared.Models.CSM
{
    public class WorkModel
    {
        [Required(ErrorMessage = "Field is required")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [MaxLength(120, ErrorMessage = "Max length is 120")]
        public string Name { get; set; }
        public List<WorkTableModel> Tables { get; set; }

        public WorkModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Tables = new List<WorkTableModel>();
        }

        public WorkModel(Work work)
        {
            this.Id = work.Id;
            this.Name = work.Name;
            this.Tables = work.Tables.Select(x => new WorkTableModel(x)).OrderBy(x => x.Date).ToList();
        }

        public void SetTables(DateTime start, DateTime finish)
        {
            var s = start;
            while (s < finish)
            {
                Tables.Add(new WorkTableModel(s));
                s = s.AddHours(1);
            }
        }

        public void UpdateTable(DateTime newStart, DateTime newFinish)
        {
            var oldList = this.Tables;
            this.Tables = new List<WorkTableModel>();

            this.SetTables(newStart, newFinish);

            foreach (var i in this.Tables)
            {
                var e = oldList.FirstOrDefault(x => DateHelper.CompareDates(x.Date, i.Date));
                if (e != null)
                {
                    i.IsActive = e.IsActive;
                }
            }
        }
    }
}
