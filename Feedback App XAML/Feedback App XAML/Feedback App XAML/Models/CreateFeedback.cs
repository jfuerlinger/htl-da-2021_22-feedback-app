using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback_App_XAML.Models
{
    internal class CreateFeedback
    {
        public string TeachingUnitId { get; set; }
        public string UserId { get; set; }
        public string Stars { get; set; }
        public string Comment { get; set; }
        public string Token { get; set; }

    }
}
