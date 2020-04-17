namespace PlanManager.Services.Messages.PM
{
    public class PlanMessages
    {
        // Invalid responses
        public readonly string InvalidPlanId = "Invalid plan Id.";
        
        // Actions
        public readonly string AllPlansGet = "get all plans";
        public readonly string MyPlansGet = "get my plans";
        public readonly string PlanGet = "get plan";
        public readonly string OtherPublicPlanGet = "get other's public plans";
        public readonly string PlanDelete = "delete plan";
        public readonly string PlanUpdate = "update plan";
        public readonly string PlanCreate = "create plan";
    }
}