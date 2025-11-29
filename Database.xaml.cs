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
            LoadDataByGET();

        }

        private void tablak_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tablak.SelectedValue.ToString() != kivalasztottTabla)
            {
                kivalasztottTabla = tablak.SelectedValue.ToString();
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

        private async Task<List<T>> BeginLoadAsync<T>(Tabla kivalasztottTabla) where T : Mezo, new()
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

            return new List<T>();

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

    }
}
