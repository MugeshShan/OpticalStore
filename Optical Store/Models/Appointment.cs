using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optical_Store.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Time { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Remarks { get; set; }

        public int PatientId { get; set; }
    }
}
