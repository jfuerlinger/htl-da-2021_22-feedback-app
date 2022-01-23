using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbackApp_WebApi.Persistance.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string IdentityId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        [MaxLength(50)]
        public string School { get; set; }
    }
}
