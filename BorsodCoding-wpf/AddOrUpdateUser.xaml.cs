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
using System.Windows.Shapes;
using ZstdSharp.Unsafe;
using BorsodCoding_WPF_Admin.Tablak;

namespace BorsodCoding_WPF_Admin
{
    /// <summary>
    /// Interaction logic for AddOrUpdateUser.xaml
    /// </summary>
    public partial class AddOrUpdateUser : Window
    {
        string _mode = "";
        string _token = "";

        public AddOrUpdateUser()
        {
            InitializeComponent();
        }

        public AddOrUpdateUser(string mode, string userToken)
        {
            InitializeComponent();
            _mode = mode;
            _token = userToken;
        }

        public AddOrUpdateUser(UserJsonBody user, string mode, string userToken)
        {
            InitializeComponent();
            _mode = mode;
            _token = userToken;
            tbxUsername.Text = user.UserName;
            tbxPassword.Text = "";
            tbxEmail.Text = user.Email;
            bAction.Content = "A rekord módosítása";
        }

        private async void bAction_Click(object sender, RoutedEventArgs e)
        {
            if (_mode == "add")
            {
                if (await new UserTabla().InsertAData(new UserJsonBody()
                {
                    UserName = tbxUsername.Text,
                    Password = tbxPassword.Text,
                    Email = tbxEmail.Text
                }, _token))
                {
                    Close();
                }
            }

            if (_mode == "modify")
            {
                if (await new UserTabla().UpdateAData(new UserJsonBody()
                {
                    UserName = tbxUsername.Text,
                    Password = tbxPassword.Text,
                    Email = tbxEmail.Text
                }, _token))
                {
                    Close();
                }
            }
        }
    }
}
