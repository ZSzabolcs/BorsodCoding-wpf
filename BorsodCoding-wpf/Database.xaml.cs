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
using BorsodCoding_WPF_Admin.JsonBodies;

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
            var userTabla = new UserTabla("user", "https://localhost:7159/auth", "https://localhost:7159/auth/register", "https://localhost:7159/auth/Modositas", "https://localhost:7159/auth?id=");
            var saveTabla = new SaveTabla("save", "https://localhost:7159/api/Save", "https://localhost:7159/api/Save", "https://localhost:7159/api/Save/FromWPF", "https://localhost:7159/api/Save?id=");
            tablaKollekcio.Add(userTabla.TablaNev, userTabla);
            tablaKollekcio.Add(saveTabla.TablaNev, saveTabla);
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
            var issuceded = await tablaKollekcio[kivalasztottTabla].DeleteAData(kivalasztottId, userToken);
            if (issuceded)
            {
                TablaBetoltes();
            }
        }

        private async void button_KivalasztottRekordModositas(object sender, RoutedEventArgs e)
        {
            tablaKollekcio[kivalasztottTabla].LoadUpdateDataWindow(userToken, (kivalasztottElem as JsonBody), tablaKollekcio[kivalasztottTabla]);
            TablaBetoltes();
            
        }

        private async void button_UjRekord(object sender, RoutedEventArgs e)
        {
            tablaKollekcio[kivalasztottTabla].LoadAddDataWindow(userToken, tablaKollekcio[kivalasztottTabla]);
            TablaBetoltes();


        }

        private void tabla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (tabla.SelectedIndex > -1)
                {
                    if (kivalasztottTabla == "user")
                    {
                        kivalasztottId = (currentTableCollection as ObservableCollection<UserMezoi>)[tabla.SelectedIndex].Id;
                        var rekord = (currentTableCollection as ObservableCollection<UserMezoi>)[tabla.SelectedIndex];
                        UserJsonBody jsonBody = new UserJsonBody()
                        {
                            Name = rekord.UserName,
                            Password = "",
                            Email = rekord.Email

                        };
                        kivalasztottElem = jsonBody;

                    }

                    if (kivalasztottTabla == "save")
                    {
                        kivalasztottId = (currentTableCollection as ObservableCollection<SaveMezoi>)[tabla.SelectedIndex].Id;
                        var rekord = (currentTableCollection as ObservableCollection<SaveMezoi>)[tabla.SelectedIndex];
                        SaveJsonBody jsonBody = new SaveJsonBody()
                        {
                            Id = kivalasztottId,
                            Points = rekord.Points,
                            Level = rekord.Level,
                            Language = rekord.Language,
                        };
                       
                        kivalasztottElem = jsonBody;
                    }

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
    }
}
