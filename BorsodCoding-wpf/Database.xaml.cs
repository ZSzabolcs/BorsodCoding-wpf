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
using System.Diagnostics.Eventing.Reader;
using BorsodCoding_WPF_Admin.AddOrUpdateWindows;

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
        readonly string  userToken = "";
        public Database(string token)
        {
            InitializeComponent();
            userToken = token;

        }

        /// <summary>
        /// MessageBox-ban kiírja a megtalált JSON mező értékét. Ha nem találja, akkor alapértelmezetten a teljes JSON szöveget kiírja.
        /// </summary>
        /// <param name="jsonString">JSON szöveg</param>
        /// <param name="property">JSON-ben keresendő mező</param>
        public static void ShowJsonProperty(string jsonString, string property, bool isSuccesfull)
        {
            bool isJsoon = jsonString.StartsWith('{');
            if (isJsoon && isSuccesfull)
            {
                JsonDocument jsonDocument = JsonDocument.Parse(jsonString);
                if (jsonDocument.RootElement.TryGetProperty(property, out var messageElement))
                {
                    string message = messageElement.GetString();
                    MessageBox.Show(message, "Infó", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (isJsoon && !isSuccesfull)
            {
            
                JsonDocument jsonDocument = JsonDocument.Parse(jsonString);
                if (jsonDocument.RootElement.TryGetProperty(property, out var messageElement))
                {
                    string message = messageElement.GetString();
                    MessageBox.Show(message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(jsonString, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }

           
        }
            


        private async void TablaBetoltes()
        {

            var adatok = await tablaKollekcio[kivalasztottTabla].GetDataFromApi(userToken);
            currentTableCollection = adatok;
            dtgTabla.ItemsSource = adatok as IEnumerable;
            dtgTabla.Items.Refresh();
            AlapAllapot();

        }

        private void tablak_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxTablak.SelectedValue.ToString() != kivalasztottTabla)
            {
                kivalasztottTabla = lbxTablak.SelectedValue.ToString();
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
            TablaBetoltes();
        }

        private async void button_KivalasztottRekordModositas(object sender, RoutedEventArgs e)
        {
            var jsonBody = tablaKollekcio[kivalasztottTabla].GetPutJson(dtgTabla.ItemsSource, dtgTabla.SelectedIndex);
            UpdateWindow updateWindow = new UpdateWindow(userToken, jsonBody, tablaKollekcio[kivalasztottTabla]);
            updateWindow.ShowDialog();
            TablaBetoltes();
            
        }

        private async void button_UjRekord(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(userToken, tablaKollekcio[kivalasztottTabla]);
            addWindow.ShowDialog();
            TablaBetoltes();


        }

        private void tabla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (dtgTabla.SelectedIndex > -1)
                {
                    string id = tablaKollekcio[kivalasztottTabla].GetSelectedItemId(currentTableCollection, dtgTabla.SelectedIndex);
                    kivalasztottId = id;
                    

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var userTabla = new UserTabla("user",
               getURL: "https://localhost:7159/auth",
               postURL: "https://localhost:7159/auth/register",
               putURL: "https://localhost:7159/auth/Modositas",
               delURL: "https://localhost:7159/auth?id=",
               new InsertUserDto()
               );
            var saveTabla = new SaveTabla("save",
                getURL: "https://localhost:7159/api/Save",
                postURL: "https://localhost:7159/api/Save",
                putURL: "https://localhost:7159/api/Save/FromWPF",
                delURL: "https://localhost:7159/api/Save?id=",
                new InsertSaveDto()
                );
            var velemenyTabla = new VelemenyTabla("vélemény",
                getURL: "https://localhost:7159/api/Velemeny",
                postURL: "https://localhost:7159/api/Velemeny",
                putURL: "https://localhost:7159/api/Velemeny/FromWPF",
                delURL: "https://localhost:7159/api/Velemeny/FromWPF?id=",
                new InsertVelemenyDto());
            tablaKollekcio.Add(userTabla.TablaNev, userTabla);
            tablaKollekcio.Add(saveTabla.TablaNev, saveTabla);
            tablaKollekcio.Add(velemenyTabla.TablaNev, velemenyTabla);
            string[] tablaNevek = new string[tablaKollekcio.Count];
            lbxTablak.ItemsSource = tablaNevek;
            int i = 0;
            foreach (var dtgTabla in tablaKollekcio.Keys)
            {
                tablaNevek[i] = dtgTabla.ToString();
                i++;
            }
            kivalasztottTabla = tablaNevek[0];
            TablaBetoltes();
        }
    }
}
