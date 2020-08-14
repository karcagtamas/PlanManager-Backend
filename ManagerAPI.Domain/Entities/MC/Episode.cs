using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.MC
{
    public class Episode : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        public string Description { get; set; }

        [Required]
        public int SeasonId { get; set; }

        public virtual Season Season { get; set; }

        public virtual ICollection<UserEpisode> ConnectedUsers { get; set; }

        override public bool Equals(object obj) {
            return obj != null && ((Episode)obj).Id == this.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Number, Description, SeasonId, Season, ConnectedUsers);
        }

        override public string ToString() {
            return $"{this.Id} - {this.Number}";
        }
    }
}
