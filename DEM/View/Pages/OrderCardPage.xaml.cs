using DEM.DataBase;
using DEM.DataBase.models;
using DEM.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DEM.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderCard.xaml
    /// </summary>
    public partial class OrderCardPage : Page
    {
        public Order Order { get; set; }
        public bool Change { get; set; }

        public OrderCardPage()
        {
            InitializeComponent();
            using (var context = new DataBaseContext())
            {
                Partner.ItemsSource = context.Partners.ToList();
                Status.ItemsSource = Enum.GetValues(typeof(Status)).Cast<Status>();
            }
            Change = false;
            Order = new Order();
            Order.OrderItems = new List<OrderItem>();
        }

        public OrderCardPage(Order order)
        {
            InitializeComponent();
            Order = order;
            using (var context = new DataBaseContext())
            {
                Partner.ItemsSource = context.Partners.ToList();
                Status.ItemsSource = Enum.GetValues(typeof(Status)).Cast<Status>();
            }
            Change = true;
            Partner.SelectedValue = order.Partner?.Id;
            Status.SelectedValue = order.Status;
            OrderDate.SelectedDate = order.OrderDate;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            UtilsProperties.CurrentFrame.Navigate(new ItemCardPage(Order));
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var context = new DataBaseContext();
            int partnerId = (Partner.SelectedItem as Partner)?.Id ?? 0;
            Status selectedStatus = (Status)Status.SelectedItem;
            DateTime? selectedDate = OrderDate.SelectedDate;

            if (partnerId == 0)
            {
                MessageBox.Show("Выберите партнёра.");
                return;
            }

            if (!selectedDate.HasValue || selectedDate == DateTime.MinValue)
            {
                MessageBox.Show("Выберите корректную дату.");
                return;
            }
            var partner = context.Partners.FirstOrDefault(p => p.Id == partnerId);
            if (partner == null)
            {
                MessageBox.Show("Партнёр не найден в базе.");
                return;
            }

            Order.Partner = partner;
            Order.OrderDate = selectedDate.Value;
            Order.Status = selectedStatus;

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
            UtilsProperties.CurrentFrame.Navigate(new MainPage());
        }
    }
}