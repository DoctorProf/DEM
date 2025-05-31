using DEM.DataBase;
using DEM.DataBase.models;
using DEM.Utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DEM.View.Pages;
using DEM.ViewModel;

namespace DEM.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderCard.xaml
    /// </summary>
    public partial class OrderCardPage : Page
    {
        public OrderCardLogic OrderCardLogic { get; set; }

        public OrderCardPage()
        {
            InitializeComponent();
            using (var context = new DataBaseContext())
            {
                Partner.ItemsSource = context.Partners.ToList();
                Status.ItemsSource = Enum.GetValues(typeof(Status)).Cast<Status>();
            }
            if (OrderCardLogic == null)
            {
                OrderCardLogic = new OrderCardLogic(new Order());
            }
            else
            {
                var order = OrderCardLogic.Order;
                Partner.SelectedValue = order.Partner?.Id;
                Status.SelectedValue = order.Status;
                OrderDate.SelectedDate = order.OrderDate;
            }
        }

        public OrderCardPage(Order order)
        {
            InitializeComponent();
            using (var context = new DataBaseContext())
            {
                Partner.ItemsSource = context.Partners.ToList();
                Status.ItemsSource = Enum.GetValues(typeof(Status)).Cast<Status>();
            }
            OrderCardLogic = new OrderCardLogic(order);
            OrderCardLogic.Change = true;
            Partner.SelectedValue = order.Partner?.Id;
            Status.SelectedValue = order.Status;
            OrderDate.SelectedDate = order.OrderDate;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            UtilsProperties.CurrentFrame.Navigate(new ItemCardPage(OrderCardLogic.Order));
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            int partnerId = (Partner.SelectedItem as Partner)?.Id ?? 0;
            Status selectedStatus = (Status)Status.SelectedItem;
            DateTime? selectedDate = OrderDate.SelectedDate;

            if (OrderCardLogic.Save(partnerId, selectedStatus, selectedDate, out string error))
            {
                MessageBox.Show("Заказ сохранён.");
                UtilsProperties.CurrentFrame.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show(error);
            }
        }
    }
}