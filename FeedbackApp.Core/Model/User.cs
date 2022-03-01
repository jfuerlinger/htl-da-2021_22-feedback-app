using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackApp.Core.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string IdentityId { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
        [MaxLength(30)]
        public string? Title { get; set; }
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        [MaxLength(50)]
        public string? School { get; set; }

        public List<TeachingUnit> TeachingUnits { get; set; } = new List<TeachingUnit>();
        public List<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
