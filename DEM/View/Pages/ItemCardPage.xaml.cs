using DEM.DataBase;
using DEM.DataBase.models;
using DEM.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DEM.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ItemCardPage.xaml
    /// </summary>
    public partial class ItemCardPage : Page
    {
        public Order Order { get; set; }

        public ItemCardPage(Order order)
        {
            InitializeComponent();
            using (var context = new DataBaseContext())
            {
                Item.ItemsSource = context.Products.ToList();
            }
            Order = order;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var context = new DataBaseContext();
            int productId = (int)Item.SelectedValue;
            string quantityStr = Quantity.Text.Trim();
            string priceStr = Price.Text.Trim();

            if (!int.TryParse(quantityStr.Trim(), out int quantity))
            {
                MessageBox.Show("Введите корректное количество (целое число).");
                return;
            }

            if (!decimal.TryParse(priceStr.Trim(), out decimal price))
            {
                MessageBox.Show("Введите корректную цену.");
                return;
            }

            var product = context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                MessageBox.Show("Товар не найден.");
                return;
            }

            var orderItem = new OrderItem
            {
                Order = Order,
                Product = product,
                Quantity = quantity,
                Price = price
            };

            Order.OrderItems.Add(orderItem);
            if (UtilsProperties.CurrentFrame.CanGoBack)
            {
                UtilsProperties.CurrentFrame.GoBack();
            }
            else
            {
                MessageBox.Show("Нельзя вернуться назад.");
            }
        }
    }
}