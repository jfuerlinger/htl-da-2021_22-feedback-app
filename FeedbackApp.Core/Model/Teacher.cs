using System;
using System.ComponentModel.DataAnnotations;

namespace FeedbackApp.Core.Model
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string IdentityId { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
        [MaxLength(30)]
        public string? Title { get; set; }
        public DateTime? Birthdate { get; set; }
        [MaxLength(50)]
        public string? School { get; set; }
    }
}
