namespace FeedbackApp.WebApi.Feedback
{
    public class ModifyFeedbackModel
    {
        public int FeedbackId { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
