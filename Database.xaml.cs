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
            TablaNevEllenorzes();
            LoadDataByGET();

        }

        private bool TablaNevEllenorzes()
        {
            if (kivalasztottTabla == "user")
            {
                lb_first.Content = "Név";
                lb_second.Content = "Jelszó";
                lb_third.Content = "E-mail";
                lb_fourth.Content = "Regisztráció ideje";
                lb_fifth.Content = "Módosítás ideje";
                return true;

            }
            if (kivalasztottTabla == "save")
            {
                lb_first.Content = "Pont";
                lb_second.Content = "Szint";
                lb_third.Content = "Nyelv";
                lb_fourth.Content = "Regisztráció ideje";
                lb_fifth.Content = "Módosítás ideje";
                return true;
            }

            return false;
        }

        private void tablak_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tablak.SelectedValue.ToString() != kivalasztottTabla)
            {
                kivalasztottTabla = tablak.SelectedValue.ToString();
                TablaNevEllenorzes();
                LoadDataByGET();
            }

        }

        private async void LoadDataByGET()
        {
            if (kivalasztottTabla == "user")
            {
                var adatok = await BeginLoadAsync<UserMezoi>(tablaKollekcio[kivalasztottTabla]);
                tabla.ItemsSource = adatok;
            }
            else
            {
                var adatok = await BeginLoadAsync<SaveMezoi>(tablaKollekcio[kivalasztottTabla]);
                tabla.ItemsSource = adatok;
            }



        }

        private async Task<ObservableCollection<T>> BeginLoadAsync<T>(Tabla kivalasztottTabla) where T : Mezo, new()
        {
            T osztaly = new T();
            if (osztaly is UserMezoi)
            {
                var data = await kivalasztottTabla.GetDataFromApi<T>();
                return data;
            }
            else if (osztaly is SaveMezoi)
            {
                var data = await kivalasztottTabla.GetDataFromApi<T>();
                return data;
            }

            return new ObservableCollection<T>();

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
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "/UserSaveData");
            var content = new StringContent("{\r\n\"name\" : \"\",\r\n\"points\" : 0,\r\n\"level\" : 0,\r\n\"language\" : \"hu\"\r\n}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }

        private void tabla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
