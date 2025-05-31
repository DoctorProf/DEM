using DEM.DataBase;
using DEM.DataBase.models;
using DEM.Logic;
using DEM.Utils;
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

namespace DEM.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ItemCardPage.xaml
    /// </summary>
    public partial class ItemCardPage : Page
    {
        public ItemCardLogic ItemCardLogic { get; set; }

        public ItemCardPage(Order order)
        {
            InitializeComponent();
            ItemCardLogic = new ItemCardLogic(order);
            using (var context = new DataBaseContext())
            {
                Item.ItemsSource = context.Products.ToList();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (ItemCardLogic.Add((int)Item.SelectedValue, Quantity.Text, Price.Text, out string error))
            {
                if (UtilsProperties.CurrentFrame.CanGoBack)
                {
                    UtilsProperties.CurrentFrame.GoBack();
                }
                else
                {
                    MessageBox.Show("Нельзя вернуться назад.");
                }
            }
            else
            {
                MessageBox.Show(error);
            }
        }
    }
}