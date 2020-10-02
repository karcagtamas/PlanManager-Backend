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
        public Dictionary<DateTime, List<Person>> Rows { get; set; }
        public string HoveredPersonId { get; set; } = "-";

        protected override void OnParametersSet()
        {
            this.CreateTable();
        }

        private void CreateTable()
        {
            this.Rows = new Dictionary<DateTime, List<Person>>();
            var works = Settings.Works.OrderBy(x => x.Name).ToList();
            for (int i = 0; i < works.Count; i++)
            {
                var tables = works[i].Tables.OrderBy(x => x.Date).ToList();
                for (int j = 0; j < tables.Count; j++)
                {
                    if (i == 0)
                    {
                        this.Rows.Add(tables[j].Date, new List<Person> { this.GetPerson(tables[j].PersonId) });
                    }
                    else
                    {
                        this.Rows[tables[j].Date].Add(this.GetPerson(tables[j].PersonId));
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

        private void Hover(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                this.HoveredPersonId = "-";
            }
            else
            {
                this.HoveredPersonId = id;
            }
        }
    }
}
