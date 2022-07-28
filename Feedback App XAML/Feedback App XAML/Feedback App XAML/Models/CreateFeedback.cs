using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback_App_XAML.Models
{
    internal class CreateFeedback
    {
        public int TeachingUnitId { get; set; }
        public int UserId { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
        public string Token { get; set; }

    }
}
