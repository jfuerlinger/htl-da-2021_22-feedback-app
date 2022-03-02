namespace FeedbackApp.WebApi.Feedback
{
    public class FeedbackModel
    {
        public int TeachingUnitId { get; set; }
        public int UserId { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
    }
}
