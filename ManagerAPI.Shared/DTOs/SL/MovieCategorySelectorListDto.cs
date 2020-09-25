namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// Movie category selector list data object
    /// </summary>
    public class MovieCategorySelectorListDto : IIdentified
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}