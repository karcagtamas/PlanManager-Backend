using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Models.CSM;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EventManager.Client.Shared.Components.CSM
{
    public partial class FullCsomorComponent
    {
        [Parameter]
        public GeneratorSettings Settings { get; set; }
        public Dictionary<DateTime, List<DisplayMember>> Rows { get; set; }
        public string HoveredMemberId { get; set; } = "-";
        public string Selected { get; set; }
        public bool SelectedIsPerson { get; set; } = false;

        protected override void OnParametersSet()
        {
            this.CreateTable();
            StateHasChanged();
        }

        private void CreateTable()
        {
            this.Rows = new Dictionary<DateTime, List<DisplayMember>>();
            if (this.SelectedIsPerson)
            {
                this.CreatePersonTable();
            }
            else
            {
                this.CreateWorkTable();
            }
        }

        private void CreatePersonTable()
        {
            var persons = Settings.Persons.OrderBy(x => x.Name).ToList();

            if (!string.IsNullOrEmpty(this.Selected))
            {
                persons = persons.Where(x => x.Id == this.Selected).ToList();
            }

            for (int i = 0; i < persons.Count; i++)
            {
                var tables = persons[i].Tables.OrderBy(x => x.Date).ToList();
                for (int j = 0; j < tables.Count; j++)
                {
                    if (i == 0)
                    {
                        this.Rows.Add(tables[j].Date, new List<DisplayMember> { new DisplayMember(this.GetWork(tables[j].WorkId)) });
                    }
                    else
                    {
                        this.Rows[tables[j].Date].Add(new DisplayMember(this.GetWork(tables[j].WorkId)));
                    }
                }
            }
        }

        private void CreateWorkTable()
        {
            var works = Settings.Works.OrderBy(x => x.Name).ToList();

            if (!string.IsNullOrEmpty(this.Selected))
            {
                works = works.Where(x => x.Id == this.Selected).ToList();
            }

            for (int i = 0; i < works.Count; i++)
            {
                var tables = works[i].Tables.OrderBy(x => x.Date).ToList();
                for (int j = 0; j < tables.Count; j++)
                {
                    if (i == 0)
                    {
                        this.Rows.Add(tables[j].Date, new List<DisplayMember> { new DisplayMember(this.GetPerson(tables[j].PersonId)) });
                    }
                    else
                    {
                        this.Rows[tables[j].Date].Add(new DisplayMember(this.GetPerson(tables[j].PersonId)));
                    }
                }
            }
        }

        private Person GetPerson(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            return this.Settings.Persons.FirstOrDefault(x => x.Id == id);
        }

        private Work GetWork(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            return this.Settings.Works.FirstOrDefault(x => x.Id == id);
        }

        private void Hover(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                this.HoveredMemberId = "-";
            }
            else
            {
                this.HoveredMemberId = id;
            }
        }

        private void SelectedWorkChanged(string val)
        {
            this.Selected = val;
            this.CreateTable();
            StateHasChanged();
        }

        private void SelectedPersonChanged(string val)
        {
            this.Selected = val;
            this.CreateTable();
            StateHasChanged();
        }

        private void TableTypeChanged(bool val)
        {
            this.SelectedIsPerson = val;
            this.Selected = null;
            this.CreateTable();
            StateHasChanged();
        }
    }

    public class DisplayMember
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public DisplayMember() { }

        public DisplayMember(Person person)
        {
            if (person == null)
            {
                this.Id = "";
                this.Name = "-";
            }
            else
            {
                this.Id = person.Id;
                this.Name = person.Name;
            }
        }

        public DisplayMember(Work work)
        {
            if (work == null)
            {
                this.Id = "";
                this.Name = "-";
            }
            else
            {
                this.Id = work.Id;
                this.Name = work.Name;
            }
        }
    }
}
