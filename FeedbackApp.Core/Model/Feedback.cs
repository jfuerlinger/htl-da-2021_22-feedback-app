using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackApp.Core.Model
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SubscriptionKey { get; set; }

    }
}
