using DEM.DataBase;
using DEM.DataBase.models;
using DEM.Utils;
using DEM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DEM.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainLogic MainLogic { get; set; }

        public MainPage()
        {
            InitializeComponent();
            MainLogic = new MainLogic(MainTable);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainLogic.LoadData();
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
            if (MainLogic.DeleteOrder((MainTable.SelectedItem as OrderItem), out string message))
            {
                MainLogic.LoadData();
            }
            else
            {
                MessageBox.Show(message);
            }
        }
    }
}