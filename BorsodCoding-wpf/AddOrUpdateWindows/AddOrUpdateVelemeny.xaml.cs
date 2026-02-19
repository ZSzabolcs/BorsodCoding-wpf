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
    /// Interaction logic for AddOrUpdateVelemeny.xaml
    /// </summary>
    public partial class AddOrUpdateVelemeny : Window
    {
        string _mode = "";
        string _token = "";
        string _id = "";
        Tabla _tabla = null;
        public AddOrUpdateVelemeny()
        {
            InitializeComponent();
        }

        public AddOrUpdateVelemeny(string token, VelemenyMezoi jsonBody, Tabla tabla, string mode = "modify")
        {
            InitializeComponent();
            _mode = mode;
            _token = token;
            _tabla = tabla;
            _id = jsonBody.Id;
            tbxMegjegyzes.Text = jsonBody.Megjegyzes;
            bUpdateOrAdd.Content = "A rekord módosítása";
            tbxUserName.Visibility = Visibility.Collapsed;
            lbUserName.Visibility = Visibility.Collapsed;
            cbxErtekeles.ItemsSource = new string[] { "1", "2", "3", "4", "5" };
            cbxErtekeles.Text = jsonBody.Ertekeles;
        }

        public AddOrUpdateVelemeny(string token, Tabla tabla, string mode = "add")
        {
            InitializeComponent();
            _mode = mode;
            _token = token;
            _tabla = tabla;
            bUpdateOrAdd.Content = "Az új rekord hozzáadása";
            cbxErtekeles.ItemsSource = new string[] { "1", "2", "3", "4", "5" };
        }

        private async void bUpdateOrAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_mode == "add")
            {
                AddVelemenyDto velemenyDto = new AddVelemenyDto()
                {
                    UserName = tbxUserName.Text,
                    Ertekeles = cbxErtekeles.Text,
                    Megjegyzes = tbxMegjegyzes.Text
                };

                var response = await _tabla.InsertAData(velemenyDto, _token);
                if ((response as HttpResponseMessage).IsSuccessStatusCode)
                {
                    Close();
                }
            }

            if (_mode == "modify")
            {

                VelemenyMezoi velemenyDto = new VelemenyMezoi()
                {
                    Id = _id,
                    Ertekeles = cbxErtekeles.Text,
                    Megjegyzes = tbxMegjegyzes.Text
                };

                var response = await _tabla.UpdateAData(velemenyDto, _token);
                if ((response as HttpResponseMessage).IsSuccessStatusCode)
                {
                    Close();
                }
            }
        }
    }
}
