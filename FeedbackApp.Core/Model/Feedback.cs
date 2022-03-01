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
        public int Stars { get; set; }
        public string? Comment { get; set; } = string.Empty;

        public int? UserId { get; set; }
        public User User { get; set; } = new User();

        public int? TeachingUnitId { get; set; }
        public TeachingUnit TeachingUnit { get; set; } = new TeachingUnit();

    }
}
