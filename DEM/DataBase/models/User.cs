using DEM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEM.DataBase.models
{
    public class User
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}