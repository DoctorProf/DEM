using DEM.DataBase;
using DEM.DataBase.models;
using DEM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DEM.Logic
{
    public class ItemCardLogic
    {
        public Order Order { get; set; }

        public ItemCardLogic(Order order)
        {
            Order = order;
        }

        public bool Add(int productId, string quantityStr, string priceStr, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!int.TryParse(quantityStr.Trim(), out int quantity))
            {
                errorMessage = "Введите корректное количество (целое число).";
                return false;
            }

            if (!decimal.TryParse(priceStr.Trim(), out decimal price))
            {
                errorMessage = "Введите корректную цену.";
                return false;
            }

            using (var context = new DataBaseContext())
            {
                var product = context.Products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                {
                    errorMessage = "Товар не найден.";
                    return false;
                }

                var orderItem = new OrderItem
                {
                    Order = Order,
                    Product = product,
                    Quantity = quantity,
                    Price = price
                };

                Order.OrderItems.Add(orderItem);
                return true;
            }
        }
    }
}