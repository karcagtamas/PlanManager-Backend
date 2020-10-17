using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
            Id = Guid.NewGuid().ToString();
            Tables = new List<WorkTableModel>();
        }

        public WorkModel(string name)
        {
            Id = Guid.NewGuid().ToString();
            Tables = new List<WorkTableModel>();
            Name = name;
        }

        public WorkModel(Work work)
        {
            Id = work.Id;
            Name = work.Name;
            Tables = work.Tables.Select(x => new WorkTableModel(x)).OrderBy(x => x.Date).ToList();
        }

        public void SetTables(DateTime start, DateTime finish)
        {
            DateTime s = start;
            while (s < finish)
            {
                Tables.Add(new WorkTableModel(s));
                s = s.AddHours(1);
            }
        }

        public void UpdateTable(DateTime newStart, DateTime newFinish)
        {
            List<WorkTableModel> oldList = Tables;
            Tables = new List<WorkTableModel>();

            SetTables(newStart, newFinish);

            foreach (WorkTableModel i in Tables)
            {
                WorkTableModel e = oldList.FirstOrDefault(x => DateHelper.CompareDates(x.Date, i.Date));
                if (e != null)
                {
                    i.IsActive = e.IsActive;
                }
            }
        }
    }
}
