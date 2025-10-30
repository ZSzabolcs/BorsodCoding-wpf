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
    /// Interaction logic for Database.xaml
    /// </summary>
    public partial class Database : Window
    {
        Dictionary<string, Tabla> tablaKollekcio = new Dictionary<string, Tabla>();
        string kivalasztottTabla = "";
        public Database()
        {
            InitializeComponent();
            tablaKollekcio.Add("user", new UserTabla());
            tablaKollekcio.Add("save", new SaveTabla());
            tablak.ItemsSource = new string[] { "user", "save" };

        }

        private void tablak_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            kivalasztottTabla = tablak.SelectedValue.ToString();
            LoadData();
            
        }

        private async void LoadData()
        {
            if (tablaKollekcio[kivalasztottTabla] is UserTabla)
            {
                var adatok = await (tablaKollekcio[kivalasztottTabla] as UserTabla).GetDataFromApi();
                tabla.ItemsSource = adatok;
            }
            else
            {
                var adatok = await (tablaKollekcio[kivalasztottTabla] as SaveTabla).GetDataFromApi();
                tabla.ItemsSource = adatok;
            }



        }
    }
}
