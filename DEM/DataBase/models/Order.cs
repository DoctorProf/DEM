using DEM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEM.DataBase.models
{
    public class Order
    {
        public int Id { get; set; }
        public int PartnerID { get; set; }
        public Partner Partner { get; set; }

        public DateTime OrderDate { get; set; }
        public Status Status { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}