using DEM.DataBase;
using DEM.Utils;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DEM.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private string Sha256(string text)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
            return string.Concat(bytes.Select(b => b.ToString("x2")));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var context = new DataBaseContext();

            string username = Username.Text.Trim();
            string password = Password.Password.Trim();

            string hash = Sha256(password);
            var user = context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hash);

            if (user != null)
            {
                UtilsProperties.CurrentUser = user;
                UtilsProperties.CurrentFrame.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("Неверный логин и/или пароль.");
            }
        }
    }
}