using BorsodCoding_WPF_Admin.Tablak;
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

namespace BorsodCoding_WPF_Admin.AddOrUpdateWindows
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private readonly string token;
        private object objectForm;
        private readonly Tabla actualTabla;

        public AddWindow(string token, Tabla tabla)
        {
            InitializeComponent();
            this.token = token;
            objectForm = tabla.mezo;
            actualTabla = tabla;
        }

        private async void NewRecord(object sender, RoutedEventArgs e)
        {

            string oszlop = "";
            foreach (var item in stpInputs.Children)
            {
                if (item is Label)
                {
                    Label label = item as Label;
                    oszlop = label.Content.ToString();
                }

                if (item is TextBox)
                {
                    var textbox = item as TextBox;
                    object content;
                    content = textbox.Text;
                    int szam;
                    if (int.TryParse(textbox.Text, out szam) && objectForm.GetType().GetProperty(oszlop).PropertyType == typeof(int))
                    {
                        content = szam;
                    }
                    objectForm.GetType().GetProperty(oszlop).SetValue(objectForm, content);
                }

                if (item is CheckBox)
                {
                    var checkbox = item as CheckBox;
                    var ertek = checkbox.IsChecked ?? false;
                    objectForm.GetType().GetProperty(oszlop).SetValue(objectForm, ertek);
                }

                if (item is DatePicker)
                {
                    var datepicker = item as DatePicker;
                    var ertek = datepicker.SelectedDate;
                    objectForm.GetType().GetProperty(oszlop).SetValue(objectForm, ertek);
                }
            }

            MessageBox.Show(objectForm.ToString());

            var resp = await actualTabla.InsertAData(objectForm, token);

            if (resp is HttpResponseMessage)
            {
                if ((resp as HttpResponseMessage).IsSuccessStatusCode)
                {
                    Close();
                }
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var oszlopok = objectForm.GetType().GetProperties();
            
            foreach (var item in oszlopok)
            {
                Type type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType;
                Label label = new Label();
                label.Content = item.Name;
                UIElement input = new UIElement();

                if (item.Name == "Id")
                {
                    continue;
                }

                if (type == typeof(string) || type == typeof(int))
                {
                    input = new TextBox();
                    (input as TextBox).Name = $"tbx{item.Name}";
                }

                if (type == typeof(DateTime))
                {
                    input = new DatePicker();
                    (input as DatePicker).Name = $"dp{item.Name}";
                    (input as DatePicker).Text = DateTime.Now.ToString();
                }

                if (type == typeof(bool))
                {
                    input = new CheckBox();
                    (input as CheckBox).Name = $"cbx{item.Name}";
                }

                stpInputs.Children.Add(label);
                stpInputs.Children.Add(input);

            }
            Button button = new Button();
            button.Margin = new Thickness(0, 15, 0, 0);
            button.Content = "Új rekord hozzáadása";
            button.Click += NewRecord;
            stpInputs.Children.Add(button);
        }
    }
}
