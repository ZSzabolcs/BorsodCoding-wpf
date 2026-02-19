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
using BorsodCoding_WPF_Admin.Tablak;
using BorsodCoding_WPF_Admin.Mezok;
using BorsodCoding_WPF_Admin;
using BorsodCoding_WPF_Admin.Dtos;

namespace BorsodCoding_WPF_Admin
{
    /// <summary>
    /// Interaction logic for Database.xaml
    /// </summary>
    public partial class Database : Window
    {
        Dictionary<string, Tabla> tablaKollekcio = new Dictionary<string, Tabla>();
        object currentTableCollection = new ObservableCollection<Mezo>();
        string kivalasztottId = "";
        string kivalasztottTabla = "";
        string userToken = "";
        object kivalasztottElem = new object();
        public Database(string token)
        {
            InitializeComponent();
            userToken = token;
            var userTabla = new UserTabla("user",
                getURL: "https://localhost:7159/auth",
                postURL: "https://localhost:7159/auth/register",
                putURL: "https://localhost:7159/auth/Modositas",
                delURL: "https://localhost:7159/auth?id="
                );
            var saveTabla = new SaveTabla("save", 
                getURL: "https://localhost:7159/api/Save",
                postURL: "https://localhost:7159/api/Save",
                putURL: "https://localhost:7159/api/Save/FromWPF",
                delURL: "https://localhost:7159/api/Save?id="
                );
            var velemenyTabla = new VelemenyTabla("vélemény");
            tablaKollekcio.Add(userTabla.TablaNev, userTabla);
            tablaKollekcio.Add(saveTabla.TablaNev, saveTabla);
            tablaKollekcio.Add(velemenyTabla.TablaNev, velemenyTabla);
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
        public static void ShowJsonProperty(string jsonString, string property)
        {
            using (JsonDocument doc = JsonDocument.Parse(jsonString))
            {
                if (doc.RootElement.TryGetProperty(property, out var messageElement))
                {
                    string message = messageElement.GetString();
                    MessageBox.Show(message, "Infó", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private async void TablaBetoltes()
        {

            var adatok = await tablaKollekcio[kivalasztottTabla].GetDataFromApi(userToken);
            currentTableCollection = adatok;
            tabla.ItemsSource = adatok as IEnumerable;
            AlapAllapot();

        }

        private void tablak_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tablak.SelectedValue.ToString() != kivalasztottTabla)
            {
                kivalasztottTabla = tablak.SelectedValue.ToString();
                kivalasztottElem = null;
                kivalasztottId = null;
                miModify.IsEnabled = false;
                miDelete.IsEnabled = false;
                TablaBetoltes();
            }

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
            var response = await tablaKollekcio[kivalasztottTabla].DeleteAData(kivalasztottId, userToken);
            if ((response as HttpResponseMessage).IsSuccessStatusCode)
            {
                TablaBetoltes();
            }
        }

        private async void button_KivalasztottRekordModositas(object sender, RoutedEventArgs e)
        {
            tablaKollekcio[kivalasztottTabla].LoadUpdateDataWindow(userToken, kivalasztottElem);
            TablaBetoltes();
            
        }

        private async void button_UjRekord(object sender, RoutedEventArgs e)
        {
            tablaKollekcio[kivalasztottTabla].LoadAddDataWindow(userToken);
            TablaBetoltes();


        }

        private void tabla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (tabla.SelectedIndex > -1)
                {
                    var currentRecord = tablaKollekcio[kivalasztottTabla].GetCurrentTableRecord(currentTableCollection, tabla.SelectedIndex);
                    kivalasztottId = currentRecord.Id;
                    kivalasztottElem = currentRecord.JsonBody;
                    

                    miModify.IsEnabled = true;
                    miDelete.IsEnabled = true;
                    miMegse.IsEnabled = true;

                }
            }
            catch (Exception)
            {
                
            }
           
           
        }

        private void AlapAllapot()
        {
            miDelete.IsEnabled = false;
            miModify.IsEnabled = false;
            miMegse.IsEnabled = false;
            kivalasztottElem = null;
            kivalasztottId = null;
        }

        private void btn_megse_Click(object sender, RoutedEventArgs e)
        {
            AlapAllapot();
        }

        private void miLogOut_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
