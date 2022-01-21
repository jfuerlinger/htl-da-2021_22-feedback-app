using System;
using System.ComponentModel.DataAnnotations;

namespace FeedbackApp_WebApi.Persistance.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime? Birthdate { get; set; }
        public string School { get; set; }
    }
}
