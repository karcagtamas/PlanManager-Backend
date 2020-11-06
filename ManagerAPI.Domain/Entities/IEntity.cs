namespace ManagerAPI.Domain.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        bool Equals(object obj);
        string ToString();
    }
}
