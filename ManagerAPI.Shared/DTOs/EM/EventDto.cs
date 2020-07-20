namespace ManagerAPI.Shared.DTOs.EM
{
    public class EventDto
    {
        public MasterEventDto MasterEvent { get; set; }
        public SportEventDto SportEvent { get; set; }
        public GtEventDto GtEvent { get; set; }
    }
}