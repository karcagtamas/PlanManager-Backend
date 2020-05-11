using System;
using System.ComponentModel.DataAnnotations;
using PlanManager.DataAccess.Entities;
using PlanManager.DataAccess.Entities.EM;

namespace PlanManager.Services.DTOs.EM
{
    public class EventActionListDto
    {
        public DateTime Date { get; set; }
        
        public string Message { get; set; }
        
        public string User { get; set; }
    }
}