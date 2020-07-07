namespace ManagerAPI.Models.Entities.MC
{
    public class UserMovie
    {
        public int MovieId { get; set; }
        public string UserId { get; set; }
        public bool Seen { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}
