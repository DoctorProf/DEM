using DEM.DataBase;
using DEM.DataBase.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DEM.ViewModel
{
    public class MainLogic
    {
        public DataGrid MainTable { get; set; }

        public MainLogic(DataGrid mainTable)
        {
            MainTable = mainTable;
        }

        public DataGridTextColumn CreateColumn(string header, string binding)
        {
            var column = new DataGridTextColumn()
            {
                Binding = new Binding(binding),
                Header = header,
                IsReadOnly = true,
            };
            return column;
        }

        public void LoadData()
        {
            using (var context = new DataBaseContext())
            {
                var items = context.OrderItems
                                     .Include(oi => oi.Order)
                                         .ThenInclude(o => o.Partner)
                                     .Include(oi => oi.Product)
                                     .ToList();
                MainTable.Columns.Clear();
                MainTable.Columns.Add(CreateColumn("Номер заказа", "Order.Id"));
                MainTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Дата заказа",
                    Binding = new Binding("Order.OrderDate") { StringFormat = "dd.MM.yyyy" }
                });
                MainTable.Columns.Add(CreateColumn("Партнёр", "Order.Partner.Name"));
                MainTable.Columns.Add(CreateColumn("Товар", "Product.Name"));
                MainTable.Columns.Add(CreateColumn("Кол-во", "Quantity"));
                MainTable.Columns.Add(CreateColumn("Цена", "Price"));
                MainTable.Columns.Add(CreateColumn("Статус", "Order.Status"));

                MainTable.ItemsSource = items;
            }
        }

        public bool DeleteOrder(OrderItem orderItem, out string message)
        {
            message = "";

            if (orderItem == null)
            {
                message = "Элемент для удаления не выбран.";
                return false;
            }

            using var context = new DataBaseContext();

            var orderToDelete = context.Orders
                                       .Include(o => o.OrderItems)
                                       .FirstOrDefault(o => o.Id == orderItem.OrderID);

            if (orderToDelete == null)
            {
                message = "Заказ не найден.";
                return false;
            }

            context.OrderItems.RemoveRange(orderToDelete.OrderItems);
            context.Orders.Remove(orderToDelete);
            context.SaveChanges();

            return true;
        }
    }
}