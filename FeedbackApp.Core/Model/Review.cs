using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackApp.Core.Model
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int Stars { get; set; }
        public string Comment { get; set; }

    }
}
