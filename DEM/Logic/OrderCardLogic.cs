using DEM.DataBase;
using DEM.DataBase.models;
using DEM.Utils;
using DEM.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DEM.ViewModel
{
    public class OrderCardLogic
    {
        public Order Order { get; set; }
        public bool Change { get; set; }

        public OrderCardLogic(Order order)
        {
            Order = order;
            Order.OrderItems = new List<OrderItem>();
            Change = false;
        }

        public bool Save(int partnerId, Status status, DateTime? selectedDate, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (partnerId == 0)
            {
                errorMessage = "Выберите партнёра.";
                return false;
            }

            if (!selectedDate.HasValue || selectedDate == DateTime.MinValue)
            {
                errorMessage = "Выберите корректную дату.";
                return false;
            }

            using (var context = new DataBaseContext())
            {
                var partner = context.Partners.FirstOrDefault(p => p.Id == partnerId);
                if (partner == null)
                {
                    errorMessage = "Партнёр не найден в базе.";
                    return false;
                }

                Order.Partner = partner;
                Order.OrderDate = selectedDate.Value;
                Order.Status = status;

                foreach (var item in Order.OrderItems)
                {
                    item.Product = context.Products.FirstOrDefault(p => p.Id == item.Product.Id);
                }

                if (Change)
                {
                    context.Orders.Update(Order);
                }
                else
                {
                    context.Orders.Add(Order);
                }
                context.SaveChanges();
            }

            return true;
        }
    }
}