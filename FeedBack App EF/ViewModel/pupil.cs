using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.ViewModel
{
    public class pupil
    {
        public int ID{ get; set; }
        public string firstname{ get; set; }
        public string lastname{ get; set; }
        public string username{ get; set; }
        public string emailaddress{ get; set; }
        public string school{ get; set; }
    }
}
