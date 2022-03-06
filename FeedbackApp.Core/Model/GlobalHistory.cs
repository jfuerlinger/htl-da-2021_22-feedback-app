using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackApp.Core.Model
{
    public class GlobalHistory
    {
        [Key]
        public int Id { get; set; }
        public int CreatedTeachingUnitsCount { get; set; }
        public int CreatedFeedbacksCount { get; set; }
    }
}
