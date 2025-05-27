using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEM.DataBase.models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MinPrice { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}