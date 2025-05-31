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
using DEM.Utils;

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

        public bool Login(string username, string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            string hash = Sha256(password);
            using (var context = new DataBaseContext())
            {
                var user = context.Users
                                  .FirstOrDefault(u => u.Username == username && u.PasswordHash == hash);

                if (user != null)
                {
                    UtilsProperties.CurrentUser = user;
                    return true;
                }
                else
                {
                    errorMessage = "Неверный логин и/или пароль.";
                    return false;
                }
            }
        }
    }
}