using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optical_Store.Models
{
    public class Appointment
    {
        public string Time { get; set; }

        public string DoctorName { get; set; }

        public string Status { get; set; }

        public string Remarks { get; set; }
    }
}
