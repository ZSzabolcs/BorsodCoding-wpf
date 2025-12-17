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

namespace BorsodCoding_WPF_Admin
{
    /// <summary>
    /// Interaction logic for Database.xaml
    /// </summary>
    public partial class Database : Window
    {
        Dictionary<string, Tabla> tablaKollekcio = new Dictionary<string, Tabla>();
        object tableCollection = new object();
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
            TablaNevEllenorzesEsBetoltes();

        }

        private async Task<bool> TablaNevEllenorzesEsBetoltes()
        {
            if (kivalasztottTabla == "user")
            {
                lb_first.Content = "Név";
                lb_second.Content = "Jelszó";
                lb_third.Content = "E-mail";
                lb_fourth.Visibility = Visibility.Collapsed;
                tbx_fourth.Visibility = Visibility.Collapsed;
                var adatok = await BeginLoadAsync<UserMezoi>();
                tableCollection = adatok;
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
                var adatok = await BeginLoadAsync<SaveMezoi>();
                tableCollection = adatok;
                tabla.ItemsSource = adatok;
 
            }
            else
            {
                return false;
            }

                return true;
        }

        private void tablak_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tablak.SelectedValue.ToString() != kivalasztottTabla)
            {
                kivalasztottTabla = tablak.SelectedValue.ToString();
                TablaNevEllenorzesEsBetoltes();
            }

        }


        private async Task<ObservableCollection<T>> BeginLoadAsync<T>() where T : Mezo, new()
        {

            var data = await tablaKollekcio[kivalasztottTabla].GetDataFromApi<T>();
            return data;
            

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

        private void button_KivalasztottRekordTorlese(object sender, RoutedEventArgs e)
        {

        }

        private void button_KivalasztottRekordModositas(object sender, RoutedEventArgs e)
        {

        }

        private async void button_UjRekord(object sender, RoutedEventArgs e)
        {
            if (kivalasztottTabla == "user")
            {
                UserJsonBody userJsonBody = new UserJsonBody() 
                { 
                    Name = tbx_first.Text,
                    Password = tbx_second.Text,
                    Email = tbx_third.Text
                };
                var user = await tablaKollekcio[kivalasztottTabla].InsertAData(userJsonBody);
                if (user.ToString() == "True")
                {
                    await TablaNevEllenorzesEsBetoltes();
                }
                else
                {
                    MessageBox.Show("Sikertelen");
                }

            }
            if (kivalasztottTabla == "save")
            {
                SaveJsonBody saveJsonBody = new SaveJsonBody()
                {
                    Name = tbx_first.Text,
                    Points = int.Parse(tbx_second.Text),
                    Level = int.Parse(tbx_third.Text),
                    Language = tbx_fourth.Text,
                };
                await tablaKollekcio[kivalasztottTabla].InsertAData(saveJsonBody);
            }
            //TablaNevEllenorzesEsBetoltes();


        }

        private void tabla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
