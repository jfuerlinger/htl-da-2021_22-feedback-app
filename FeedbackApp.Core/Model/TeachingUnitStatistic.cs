using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackApp.Core.Model
{
    public class TeachingUnitStatistic
    {
        [Key]
        public int Id { get; set; }
        public int FeedbackCount { get; set; }
        public double AvgStars { get; set; }
        public int TeachingUnitId { get; set; }
        public TeachingUnit teachingUnit { get; set; } = new TeachingUnit();
    }
}
