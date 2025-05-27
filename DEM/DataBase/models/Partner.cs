using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEM.DataBase.models
{
    public class Partner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string INN { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Rating { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}