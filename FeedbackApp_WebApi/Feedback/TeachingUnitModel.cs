namespace FeedbackApp.WebApi.Feedback
{
    public class TeachingUnitModel
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public bool IsPublic { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string DateString { get; set; }
        public string ExpiryDateString { get; set; }
        public string SubscriptionKey { get; set; }
    }
}
