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
            tablaKollekcio.Add(new UserTabla().tablaNev, new UserTabla());
            tablaKollekcio.Add(new SaveTabla().tablaNev, new SaveTabla());
            tablak.ItemsSource = new string[] { new UserTabla().tablaNev, new SaveTabla().tablaNev };

        }

        private void tablak_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            kivalasztottTabla = tablak.SelectedValue.ToString();
            //LoadDataBySQLSelect();
            LoadDataByGET();
            
        }

       
        


        private void LoadDataBySQLSelect()
        {
            ConnectToDatabase.connection.Open();
            var data = new MySqlCommand($"SELECT * FROM {kivalasztottTabla}", ConnectToDatabase.connection).ExecuteReader();
            if (kivalasztottTabla == "save")
            {
                List<SaveMezoi> save = tablaKollekcio[kivalasztottTabla].GetDataBySQLSelect<SaveMezoi>(data);
                tabla.ItemsSource = save;
            }
            else if (kivalasztottTabla == "user")
            {
                List<UserMezoi> user = tablaKollekcio[kivalasztottTabla].GetDataBySQLSelect<UserMezoi>(data);
                tabla.ItemsSource = user;
            }

                /*
                if (kivalasztottTabla == "user")
                {
                    List<UserMezoi> userTabla = (tablaKollekcio[kivalasztottTabla] as UserTabla).GetDataBySQL(data);
                    tabla.ItemsSource = userTabla;
                }
                else if (kivalasztottTabla == "save")
                {
                    List<SaveMezoi> saveTabla = (tablaKollekcio[kivalasztottTabla] as SaveTabla).GetDataBySQL(data);
                    tabla.ItemsSource = saveTabla;
                }
                */
                ConnectToDatabase.connection.Close();
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
                var data = await kivalasztottTabla.GetDataFromApi<T>(kivalasztottTabla.apiUrl);
                return data;
            }
            else if (osztaly is SaveMezoi)
            {
                var data = await kivalasztottTabla.GetDataFromApi<T>(kivalasztottTabla.apiUrl);
                return data;
            }

            return new List<T>();

        }

       
    }
}
