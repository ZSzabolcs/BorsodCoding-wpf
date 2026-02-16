using BorsodCoding_WPF_Admin.JsonBodies;
using BorsodCoding_WPF_Admin.Tablak;
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

namespace BorsodCoding_WPF_Admin
{
    /// <summary>
    /// Interaction logic for AddOrUpdateUser.xaml
    /// </summary>
    public partial class AddOrUpdateUser : Window
    {
        public AddOrUpdateUser()
        {
            InitializeComponent();
        }

        public AddOrUpdateUser(string token, UserJsonBody jsonBody, Tabla table)
        {
            InitializeComponent();
        }
    }
}
