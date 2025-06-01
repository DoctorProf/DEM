using DEM.DataBase;
using DEM.DataBase.models;
using DEM.Utils;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DEM.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
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
            var context = new DataBaseContext();

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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            UtilsProperties.CurrentFrame.Navigate(new OrderCardPage());
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            var orderItem = (MainTable.SelectedItem as OrderItem);
            if (orderItem == null)
            {
                MessageBox.Show("Элемент не выбран");
            }
            var order = orderItem.Order;
            UtilsProperties.CurrentFrame.Navigate(new OrderCardPage(order));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            using var context = new DataBaseContext();

            OrderItem orderItem = (MainTable.SelectedItem as OrderItem);
            if (orderItem == null)
            {
                MessageBox.Show("Элемент для удаления не выбран.");
                return;
            }

            var orderToDelete = context.Orders
                                       .Include(o => o.OrderItems)
                                       .FirstOrDefault(o => o.Id == orderItem.OrderID);
            if (orderToDelete == null)
            {
                MessageBox.Show("Заказ не найден.");
                return;
            }
            context.OrderItems.RemoveRange(orderToDelete.OrderItems);
            context.Orders.Remove(orderToDelete);
            context.SaveChanges();
            LoadData();
        }
    }
}