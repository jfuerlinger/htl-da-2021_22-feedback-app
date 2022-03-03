using System;

namespace FeedbackApp.WebApi.Feedback
{
    public class ModifyTuModel
    {
        public int TeachingUnitId { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string SubscriptionKey { get; set; }
    }
}
