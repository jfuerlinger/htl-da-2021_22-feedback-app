using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackApp.Core.Model
{
    public class TeachingUnit
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Subject { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? SubscriptionKey { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User User { get; set; } = new User();

    }
}
