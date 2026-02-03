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
        public AddOrUpdateSave()
        {
            InitializeComponent();
        }
        public AddOrUpdateSave(SaveJsonBody save, string mode, string token)
        {
            InitializeComponent();
            _mode = mode;
            _token = token;
            _id = save.Id;
            cbxLanguage.ItemsSource = new string[2] { "hu", "en" };
            if (mode == "modify")
            {
                lbName.Visibility = Visibility.Collapsed;
                tbxName.Visibility = Visibility.Collapsed;
                tbxPoints.Text = save.Points.ToString();
                tbxLevel.Text = save.Level.ToString();
                cbxLanguage.Text = save.Language;
                bSave.Content = "A rekord módosítása";
            }
        }
        public AddOrUpdateSave(string mode, string token)
        {
            InitializeComponent();
            _mode = mode;
            _token = token;
            cbxLanguage.ItemsSource = new string[2] { "hu", "en" };
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (_mode == "add")
                {
                    SaveTabla saveTabla = new SaveTabla();
                    if (await saveTabla.InsertAData(new SaveJsonBody()
                    {
                        Name = tbxName.Text,
                        Points = int.Parse(tbxPoints.Text),
                        Level = int.Parse(tbxLevel.Text),
                        Language = cbxLanguage.Text
                    }, _token))
                    {
                        Close();
                    }
                }

                if (_mode == "modify")
                {
                    SaveTabla saveTabla = new SaveTabla();
                    if (await saveTabla.UpdateAData(new SaveJsonBody() { 
                        Id = _id, 
                        Points = int.Parse(tbxPoints.Text),
                        Level = int.Parse(tbxLevel.Text),
                        Language = cbxLanguage.Text
                    }, _token))
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
