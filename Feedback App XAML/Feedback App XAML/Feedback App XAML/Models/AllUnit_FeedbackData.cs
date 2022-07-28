using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback_App_XAML.Models
{
    public class AllUnit_FeedbackData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string SubscriptionKey { get; set; }
        public int CreatedTeachingUnitsCount { get; set; }
        public int CreatedFeedbacksCount { get; set; }
        public double AvgStars { get; set; }
    }
}
