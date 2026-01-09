using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections;

namespace BorsodCoding_WPF_Admin
{
    /// <summary>
    /// Interaction logic for Database.xaml
    /// </summary>
    public partial class Database : Window
    {
        Dictionary<string, Tabla> tablaKollekcio = new Dictionary<string, Tabla>();
        object curenttableCollection = new object();
        Guid kivalasztottId = Guid.Empty;
        string kivalasztottTabla = "";
        public Database()
        {
            InitializeComponent();
            tablaKollekcio.Add(new UserTabla().TablaNev, new UserTabla());
            tablaKollekcio.Add(new SaveTabla().TablaNev, new SaveTabla());
            string[] tablaNevek = new string[tablaKollekcio.Count];
            tablak.ItemsSource = tablaNevek;
            int i = 0;
            foreach (var tabla in tablaKollekcio.Keys)
            {
                tablaNevek[i] = tabla.ToString();
                i++;
            }
            kivalasztottTabla = tablaNevek[0];
            TablaBetoltes();

        }

        private async void TablaBetoltes()
        {

            if (kivalasztottTabla == "user")
            {

                lb_first.Content = "Név";
                lb_second.Content = "Jelszó";
                lb_third.Content = "E-mail";
                lb_fourth.Visibility = Visibility.Collapsed;
                tbx_fourth.Visibility = Visibility.Collapsed;
                var adatok = await tablaKollekcio[kivalasztottTabla].GetDataFromApi<UserMezoi>();
                curenttableCollection = adatok;
                tabla.ItemsSource = adatok;

            }
            else if (kivalasztottTabla == "save")
            {
                lb_first.Content = "Név";
                lb_second.Content = "Pont";
                lb_third.Content = "Szint";
                lb_fourth.Content = "Nyelv";
                lb_fourth.Visibility = Visibility.Visible;
                tbx_fourth.Visibility = Visibility.Visible;
                var adatok = await tablaKollekcio[kivalasztottTabla].GetDataFromApi<SaveMezoi>();
                curenttableCollection = adatok;
                tabla.ItemsSource = adatok;

            }
            AlapAllapot();

        }

        private void tablak_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tablak.SelectedValue.ToString() != kivalasztottTabla)
            {
                kivalasztottTabla = tablak.SelectedValue.ToString();
                TablaBetoltes();
            }

            tbx_first.IsEnabled = true;

        }



        private void tabla_OszlopBeallitas(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                if (e.Column is DataGridTextColumn textColumn)
                {
                    var binding = (Binding)textColumn.Binding;

                    binding.StringFormat = "yyyy-MM-dd HH:mm:ss";
                }
            }
        }

        private async void button_KivalasztottRekordTorlese(object sender, RoutedEventArgs e)
        {
            var issuceded = await tablaKollekcio[kivalasztottTabla].DeleteAData(kivalasztottId);
            if (issuceded)
            {
                TablaBetoltes();
            }
        }

        private async void button_KivalasztottRekordModositas(object sender, RoutedEventArgs e)
        {
            object json = new object();
            if (kivalasztottTabla == "user")
            {
                var aktualisTabla = curenttableCollection as ObservableCollection<UserMezoi>;
                var elem = aktualisTabla[(tabla.SelectedIndex >= 0 ? tabla.SelectedIndex : 0)];
                json = new UserJsonBody
                {
                    Name = elem.Name,
                    Password = tbx_second.Text,
                    Email = tbx_third.Text,
                };
            }
            if (kivalasztottTabla == "save")
            {
                var aktualisTabla = curenttableCollection as ObservableCollection<SaveMezoi>;
                var elem = aktualisTabla[(tabla.SelectedIndex >= 0 ? tabla.SelectedIndex : 0)];
                json = new SaveJsonBody
                {
                    Id = elem.Id,
                    Points = int.Parse(tbx_second.Text),
                    Level = int.Parse(tbx_third.Text),
                    Language = tbx_fourth.Text,

                };
            }

            var issuceded = await tablaKollekcio[kivalasztottTabla].UpdateAData(json);
            if (issuceded)
            {
                TablaBetoltes();
            }
        }

        private async void button_UjRekord(object sender, RoutedEventArgs e)
        {
            object json = new object();
            if (kivalasztottTabla == "user")
            {
                json = new UserJsonBody() 
                { 
                    Name = tbx_first.Text,
                    Password = tbx_second.Text,
                    Email = tbx_third.Text
                };
                
               
            }
            if (kivalasztottTabla == "save")
            {
                json = new SaveJsonBody()
                {
                    Name = tbx_first.Text,
                    Points = int.Parse(tbx_second.Text),
                    Level = int.Parse(tbx_third.Text),
                    Language = tbx_fourth.Text,
                };
                
               
            }
            var isSucceded = await tablaKollekcio[kivalasztottTabla].InsertAData(json);

            if (isSucceded)
            {
                TablaBetoltes();
            }

        }

        private void tabla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {


                if (kivalasztottTabla == "user")
                {
                    var jelenlegitabla = curenttableCollection as ObservableCollection<UserMezoi>;
                    var elem = jelenlegitabla[(tabla.SelectedIndex >= 0 ? tabla.SelectedIndex : 0)];
                    kivalasztottId = elem.Id;
                    tbx_second.Text = elem.Password;
                    tbx_third.Text = elem.Email;

                }

                if (kivalasztottTabla == "save")
                {
                    var jelenlegitabla = curenttableCollection as ObservableCollection<SaveMezoi>;
                    var elem = jelenlegitabla[(tabla.SelectedIndex >= 0 ? tabla.SelectedIndex : 0)];
                    kivalasztottId = elem.Id;
                    tbx_second.Text = elem.Points.ToString();
                    tbx_third.Text = elem.Level.ToString();
                    tbx_fourth.Text = elem.Language;

                }

                btn_ujrekord.IsEnabled = false;
                btn_rekordModositas.IsEnabled = true;
                btn_rekordTorles.IsEnabled = true;
                btn_megse.Visibility = Visibility.Visible;
                tbx_first.Clear();
                tbx_first.IsEnabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void AlapAllapot()
        {
            btn_rekordModositas.IsEnabled = false;
            btn_rekordTorles.IsEnabled = false;
            btn_ujrekord.IsEnabled = true;
            tbx_first.Clear();
            tbx_second.Clear();
            tbx_third.Clear();
            tbx_fourth.Clear();
            btn_megse.Visibility = Visibility.Collapsed;
            tbx_first.IsEnabled = true;
        }

        private void btn_megse_Click(object sender, RoutedEventArgs e)
        {
            AlapAllapot();
        }
    }
}
