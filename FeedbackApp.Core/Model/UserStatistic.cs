using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackApp.Core.Model
{
    public class UserStatistic
    {
        [Key]
        public int Id { get; set; }
        public int CreatedTeachingUnitsCount { get; set; }
        public int CreatedFeedbacksCount { get; set; }
        public double AvgStars { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = new User();
    }
}
