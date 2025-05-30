using DEM.DataBase;
using DEM.DataBase.models;
using DEM.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace DEM.ViewModel
{
    public class AuthLogic
    {
        public AuthLogic()
        {
        }

        private string Sha256(string text)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
            return string.Concat(bytes.Select(b => b.ToString("x2")));
        }

        public void Login(string username, string password)
        {
            string hash = Sha256(password);
            using (var context = new DataBaseContext())
            {
                var user = context.Users
                                  .FirstOrDefault(u => u.Username == username && u.PasswordHash == hash);

                if (user != null)
                {
                    Utils.UtilsProperties.CurrentUser = user;
                    Utils.UtilsProperties.CurrentFrame.Navigate(new Main());
                }
                else
                {
                    MessageBox.Show("Неверный логин и/или пароль");
                }
            }
        }
    }
}