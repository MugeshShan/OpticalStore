using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optical_Store.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int Amount { get; set; }

        public string Description { get; set; }
    }
}
