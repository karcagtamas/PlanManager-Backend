namespace ManagerAPI.Shared.DTOs.SL
{
    public class MovieCategorySelectorListDto : IIdentified
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}