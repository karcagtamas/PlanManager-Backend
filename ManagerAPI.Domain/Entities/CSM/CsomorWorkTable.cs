﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.CSM
{
    public class CsomorWorkTable
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public DateTime Date;
        
        [Required]
        public string WorkId { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
        
        public string PersonId { get; set; }
        
        public virtual CsomorWork Work { get; set; }
        public virtual CsomorPerson Person { get; set; }
    }
}