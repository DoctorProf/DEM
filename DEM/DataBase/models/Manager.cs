using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEM.DataBase.models
{
    public class Manager
    {
        public int Id { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}