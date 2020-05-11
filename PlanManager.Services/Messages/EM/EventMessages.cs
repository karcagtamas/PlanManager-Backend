namespace PlanManager.Services.Messages.EM
{
    public class EventMessages
    {
        // Actions
        public readonly string AllMyEventGet = "get all my events";
        public readonly string EventGet = "get event";
        public readonly string MasterEventCreate = "create master event";
        public readonly string DGtEventCreate = "create gt event";
        public readonly string DSportEventCreate = "create sport event";
        public readonly string EventDelete = "delete event";
        public readonly string MasterEventUpdate = "update master event";
        public readonly string DGtEventUpdate = "update gt event";
        public readonly string DSportEventUpdate = "update sport event";
        public readonly string EventActionsGet = "get event actions";
        
        // Invalid
        public readonly string InvalidEventId = "Invalid event Id.";
    }
}