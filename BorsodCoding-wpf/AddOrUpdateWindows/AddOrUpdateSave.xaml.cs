using BorsodCoding_WPF_Admin;
using BorsodCoding_WPF_Admin.Mezok;
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
using static Mysqlx.Notice.Warning.Types;
using System.Net.Http;
using BorsodCoding_WPF_Admin.Dtos;

namespace BorsodCoding_WPF_Admin
{
    /// <summary>
    /// Interaction logic for AddOrUpdateSave.xaml
    /// </summary>
    public partial class AddOrUpdateSave : Window
    {
        string _mode = "";
        string _token = "";
        string _id = "";
        Tabla _tabla = null;
        public AddOrUpdateSave()
        {
            InitializeComponent();
        }
        public AddOrUpdateSave(string token, PutSaveDto save, Tabla tabla, string mode = "modify")
        {
            InitializeComponent();
            _mode = mode;
            _token = token;
            _id = save.Id;
            _tabla = tabla;
            cbxLanguage.ItemsSource = new string[2] { "hu", "en" };
            
            lbName.Visibility = Visibility.Collapsed;
            tbxName.Visibility = Visibility.Collapsed;
            tbxPoints.Text = save.Points.ToString();
            tbxLevel.Text = save.Level.ToString();
            cbxLanguage.Text = save.Language;
            bSave.Content = "A rekord módosítása";
            
        }
        public AddOrUpdateSave(string token, Tabla tabla, string mode = "add")
        {
            InitializeComponent();
            _mode = mode;
            _token = token;
            _tabla = tabla;
            cbxLanguage.ItemsSource = new string[2] { "hu", "en" };
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (_mode == "add")
                {
                    var save = new InsertSaveDto()
                    {
                        Name = tbxName.Text,
                        Points = int.Parse(tbxPoints.Text),
                        Level = int.Parse(tbxLevel.Text),
                        Language = cbxLanguage.Text
                    };
                    var response = await _tabla.InsertAData(save, _token);
                    var jsonString = await (response as HttpResponseMessage).Content.ReadAsStringAsync();
                    if ((response as HttpResponseMessage).IsSuccessStatusCode)
                    {

                        Close();
                    }
                }

                if (_mode == "modify")
                {
                    var save = new PutSaveDto()
                    {
                        Id = _id,
                        Points = int.Parse(tbxPoints.Text),
                        Level = int.Parse(tbxLevel.Text),
                        Language = cbxLanguage.Text
                    };
                    var response = await _tabla.UpdateAData(save, _token);
                    var jsonString = await (response as HttpResponseMessage).Content.ReadAsStringAsync();
                    if ((response as HttpResponseMessage).IsSuccessStatusCode)
                    {

                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
