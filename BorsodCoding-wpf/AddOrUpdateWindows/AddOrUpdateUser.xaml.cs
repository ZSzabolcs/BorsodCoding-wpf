using BorsodCoding_WPF_Admin.Dtos;
using BorsodCoding_WPF_Admin.Mezok;
using BorsodCoding_WPF_Admin.Tablak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BorsodCoding_WPF_Admin
{
    /// <summary>
    /// Interaction logic for AddOrUpdateUser.xaml
    /// </summary>
    public partial class AddOrUpdateUser : Window
    {
        string _mode = "";
        string _token = "";
        string _id = "";
        Tabla _tabla = null;
        string _userName = "";
        public AddOrUpdateUser()
        {
            InitializeComponent();
        }

        public AddOrUpdateUser(string token, UserDto jsonBody, Tabla tabla, string mode = "modify")
        {
            InitializeComponent();
            _mode = mode;
            _token = token;
            _tabla = tabla;
            _userName = jsonBody.UserName;
            bUjVagyMod.Content = "A rekord módosítása";
            lbUserName.Visibility = Visibility.Collapsed;
            tbxFelh.Visibility = Visibility.Collapsed;
            tbxEmail.Text = jsonBody.Email;
        }

        public AddOrUpdateUser(string token, Tabla tabla, string mode = "add")
        {
            InitializeComponent();
            _mode = mode;
            _token = token;
            _tabla = tabla;
            bUjVagyMod.Content = "Az új rekord hozzáadása";
        }

        private async void bUjVagyMod_Click(object sender, RoutedEventArgs e)
        {
            if (_mode == "add")
            {
                var user = new InsertUserDto()
                {
                    UserName = tbxFelh.Text,
                    Email = tbxEmail.Text,
                    Password = tbxPassw.Text,
                }; 
                var response = await _tabla.InsertAData(user, _token);
                if((response as HttpResponseMessage).IsSuccessStatusCode)
                {
                    Close();
                }
                

               
            }

            if (_mode == "modify")
            {
                var user = new InsertUserDto()
                {
                   UserName = _userName,
                   Email = tbxEmail.Text,
                   Password = tbxPassw.Text,
                };
                var response = await _tabla.UpdateAData(user, _token);
                if ((response as HttpResponseMessage).IsSuccessStatusCode)
                {
                    Close();
                }
            }
        }
    }
}
