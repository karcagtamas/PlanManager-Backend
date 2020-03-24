using System;

namespace PlanManager.DataAccess.Entities
{
    public class UserPlanGroup
    {
        public string UserId { get; set; }
        public int GroupId { get; set; }
        public int RoleId { get; set; }
        public DateTime Connection { get; set; }
        public string AddedById { get; set; }

        public virtual User User { get; set; }

        public virtual PlanGroup Group { get; set; }

        public virtual GroupRole Role { get; set; }

        public virtual User AddedBy { get; set; }
    }
}